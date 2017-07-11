﻿using System.Collections.Generic;
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

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        readonly FilesHelper _filesHelper;
        readonly string _tempPath = "~/somefiles/";
        readonly string _serverMapPath = "~/Files/somefiles/";
        private string StorageRoot => Path.Combine(HostingEnvironment.MapPath(_serverMapPath));

        private readonly string _urlBase = "/Files/somefiles/";
        readonly string _deleteUrl = "/FileUpload/DeleteFile/?file=";
        readonly string _deleteType = "GET";

        public PhotoAlbumController()
        {
            _filesHelper = new FilesHelper(_deleteUrl, _deleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
        }

        [HttpPost]
        public JsonResult Upload()
        {
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
    }
}