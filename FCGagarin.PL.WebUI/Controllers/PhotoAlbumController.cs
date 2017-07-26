using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;
using FCGagarin.PL.WebUI.Helpers;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class PhotoAlbumController : Controller
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService, IPhotoService photoService)
        {
            _photoAlbumService = photoAlbumService;
        }

        FilesHelper _filesHelper;
        readonly string _tempPath = "~/albums/";
        readonly string _serverMapPath = "~/Data/uploads/albums/";
        private string StorageRoot => Path.Combine(HostingEnvironment.MapPath(_serverMapPath));

        private readonly string _urlBase = "/Data/uploads/albums/";
        readonly string _deleteUrl = "/PhotoAlbum/DeleteFile/";
        readonly string _deleteType = "GET";

        

        [HttpPost]
        public JsonResult Upload(int id)
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
            var resultList = new List<ViewDataUploadFilesResult>();

            var currentContext = HttpContext;

            _filesHelper.UploadAndShowResults(currentContext, resultList, id);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            return isEmpty ? Json("Error ") : Json(files);
        }

        public JsonResult GetFileList(int id)
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
            var list = _filesHelper.GetFileList(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteFile(string file, int id)
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
            _filesHelper.DeleteFile(file, id);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var allAlbums = _photoAlbumService.GetAll();
            var model = Mapper.Map<IEnumerable<PhotoAlbum>, IEnumerable<PhotoAlbumViewModel>>(allAlbums);
            foreach (var photoAlbumViewModel in model)
            {
                photoAlbumViewModel.NumberOfPhoto = GetNumberOfPhoto(photoAlbumViewModel.Id);
            }
            
            return View(model);
        }

        private int GetNumberOfPhoto(int albumId)
        {
            var pathToAlbum = GetPathToAlbum(albumId);
            if (Directory.Exists(pathToAlbum))
            {
                return Directory.GetFiles(GetPathToAlbum(albumId), "*", SearchOption.TopDirectoryOnly).Length;
            }
            return 0;
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Create(PhotoAlbumFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var newAlbum = Mapper.Map<PhotoAlbumFormModel, PhotoAlbum>(formModel);
                _photoAlbumService.Create(newAlbum);
                return RedirectToAction("Index");
            }

            return View(formModel);
        }

        public ActionResult Details(int id)
        {
            var album = _photoAlbumService.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            
            var viewModel = Mapper.Map<PhotoAlbum, PhotoAlbumDetailsViewModel>(album);
            
            viewModel.FilesViewModel = ConvertToFilesViewModel(id);

            return View(viewModel);
        }

        private FilesViewModel ConvertToFilesViewModel(int albumId)
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);

            var fullPathToAlbum = _photoAlbumService.GetFullPathToAlbum(albumId);

            var result = new List<ViewDataUploadFilesResult>();
            if (Directory.Exists(fullPathToAlbum))
            {
                var dir = new DirectoryInfo(fullPathToAlbum);
                foreach (var file in dir.GetFiles())
                {
                    var sizeInt = unchecked((int)file.Length);
                    result.Add(_filesHelper.UploadResult(file.Name, sizeInt, file.FullName, albumId));
                }
            }

            var model = new FilesViewModel
            {
                files = result.ToArray()
            };
            return model;
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {
            var model = _photoAlbumService.GetById(id);
            if (model != null)
            {
                var formModel = Mapper.Map<PhotoAlbum, PhotoAlbumFormModel>(model);
                return View(formModel);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Edit(PhotoAlbumFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<PhotoAlbumFormModel, PhotoAlbum>(formModel);
                //TODO:Kireev. Альбомы храним так: AlbumId - это папка с альбомом. Название альбома хранится в бд
                _photoAlbumService.Update(model);
                return RedirectToAction("Index");
            }
            return View(formModel);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Delete(int id)
        {
            var model = _photoAlbumService.GetById(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<PhotoAlbum, PhotoAlbumViewModel>(model);
                return View(viewModel);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = _photoAlbumService.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            //удаляем все файлы вместе с папкой альбома
            var pathToAlbum = GetPathToAlbum(id);
            Directory.Delete(pathToAlbum, true);
            //удаляем альбом из бд
            _photoAlbumService.Delete(model);
            return RedirectToAction("Index");
        }

        private string GetPathToAlbum(int albumId)
        {
            return Path.Combine(StorageRoot, albumId.ToString());
        }
    }
}