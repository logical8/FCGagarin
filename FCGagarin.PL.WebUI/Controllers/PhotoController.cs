using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;
using FCGagarin.PL.WebUI.Extensions;
using FCGagarin.PL.WebUI.Helpers;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        FilesHelper _filesHelper;
        readonly string _tempPath = "~/somefiles/";
        readonly string _serverMapPath = "~/Files/somefiles/";
        private string StorageRoot => Path.Combine(HostingEnvironment.MapPath(_serverMapPath));

        private readonly string _urlBase = "/Files/somefiles/";
        readonly string _deleteUrl = "/FileUpload/DeleteFile/?file=";
        readonly string _deleteType = "GET";

        public PhotoController()
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
        }

        [HttpPost]
        public JsonResult Upload()
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
            var resultList = new List<ViewDataUploadFilesResult>();

            var currentContext = HttpContext;

            _filesHelper.UploadAndShowResults(currentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            return isEmpty ? Json("Error ") : Json(files);
        }

        public JsonResult GetFileList()
        {
            var list = _filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            _filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        ///////////////////////////////////////////////////////////////////////////////////////


        public ActionResult Index()
        {
            var allAlbums = _photoAlbumService.GetAll();
            var model = Mapper.Map<IEnumerable<PhotoAlbum>, IEnumerable<PhotoAlbumViewModel>>(allAlbums);

            return View(model);
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

            JsonFiles listOfFiles = _filesHelper.GetFileList();

            viewModel.FilesViewModel = new FilesViewModel
            {
                Files = listOfFiles.Files
            };

            //не уверен, что это нужно
            foreach (var item in viewModel.PhotoViewModelList)
            {
                item.AlbumId = id;
                item.PathToImage = "";//TODO:Тут доделать //LinkPrepare.YoutubeUrlToEmbedUrl(item.Url);
            }

            return View(viewModel);
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
            //TODO:Kireev. реализовать в бд каскадное удаление для фотографий альбома
            _photoAlbumService.Delete(model);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Moderator")]
        //public ActionResult Create(int? albumId)
        //{
        //    var album = _photoAlbumService.GetById(albumId.GetValueOrDefault());
        //    if (album == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(new PhotoFormModel { AlbumId = albumId.GetValueOrDefault(), AlbumName = album.Name });
        //}

        //[Authorize(Roles = "Moderator")]
        //[HttpPost]
        //public ActionResult Create(PhotoFormModel formModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newPhoto = Mapper.Map<PhotoFormModel, Photo>(formModel);
        //        newPhoto.AuthorId = User.Identity.GetUserProfile().Id;
        //        using (var db = new FCGagarinContext())
        //        {
        //            db.Photos.Add(newPhoto);
        //            db.SaveChanges();
        //        }
        //        return RedirectToAction("Details", "PhotoAlbum", new { id = newPhoto.AlbumId });
        //    }

        //    return View(formModel);
        //}

        //[Authorize(Roles = "Moderator")]
        //public ActionResult Edit(int id)
        //{
        //    using (var db = new FCGagarinContext())
        //    {
        //        var model = db.Photos.Find(id);
        //        if (model != null)
        //        {
        //            var formModel = Mapper.Map<Photo, PhotoFormModel>(model);
        //            return View(formModel);
        //        }
        //        return HttpNotFound();
        //    }
        //}

        //[Authorize(Roles = "Moderator")]
        //[HttpPost]
        //public ActionResult Edit(PhotoFormModel formModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new FCGagarinContext())
        //        {
        //            var model = Mapper.Map<PhotoFormModel, Photo>(formModel);
        //            db.Entry(model).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Details", "PhotoAlbum", new { id = model.AlbumId });
        //        }
        //    }
        //    return View(formModel);
        //}

        //[Authorize(Roles = "Moderator")]
        //public ActionResult Delete(int id)
        //{
        //    using (var db = new FCGagarinContext())
        //    {
        //        var model = db.Photos
        //            .Include("Author")
        //            .FirstOrDefault(x => x.Id == id);
        //        if (model != null)
        //        {
        //            var viewModel = Mapper.Map<Photo, PhotoViewModel>(model);
        //            return View(viewModel);
        //        }
        //        return HttpNotFound();
        //    }
        //}
        //[Authorize(Roles = "Moderator")]
        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    using (var db = new FCGagarinContext())
        //    {
        //        var photo = db.Photos.Find(id);
        //        if (photo == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        db.Photos.Remove(photo);
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "PhotoAlbum", new { id = photo.AlbumId });
        //    }
        //}

        //public ActionResult Upload()
        //{
        //    {
        //        var isSavedSuccessfully = true;
        //        var fName = "";
        //        try
        //        {
        //            foreach (string fileName in Request.Files)
        //            {
        //                var file = Request.Files[fileName];
        //                //Save file content goes here
        //                fName = file.FileName;
        //                if (file != null && file.ContentLength > 0)
        //                {

        //                    var originalDirectory =
        //                        new DirectoryInfo($"{Server.MapPath(@"\")}Images\\WallImages");

        //                    var pathString = Path.Combine(originalDirectory.ToString(), "imagepath");

        //                    var fileName1 = Path.GetFileName(file.FileName);

        //                    var isExists = Directory.Exists(pathString);

        //                    if (!isExists)
        //                        Directory.CreateDirectory(pathString);

        //                    var path = $"{pathString}\\{file.FileName}";
        //                    file.SaveAs(path);

        //                }

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            isSavedSuccessfully = false;
        //        }


        //        if (isSavedSuccessfully)
        //        {
        //            return Json(new {Message = fName});
        //        }
        //        return Json(new {Message = "Error in saving file"});
        //    }
        //}
    }
}