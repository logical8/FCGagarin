using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class PhotoAlbumController : Controller
    {
        private IPhotoAlbumService _photoAlbumService;

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

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