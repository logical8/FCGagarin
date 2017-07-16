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
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public ActionResult Index()
        {
            var allAlbums = _photoService.GetAll();
            var model = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(allAlbums);

            return View(model);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Create(PhotoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var newAlbum = Mapper.Map<PhotoFormModel, Photo>(formModel);
                _photoService.Create(newAlbum);
                return RedirectToAction("Index");
            }

            return View(formModel);
        }

        public ActionResult Details(int id)
        {
            var album = _photoService.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {

            var model = _photoService.GetById(id);
            if (model != null)
            {
                var formModel = Mapper.Map<Photo, PhotoFormModel>(model);
                return View(formModel);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Edit(PhotoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var model = Mapper.Map<PhotoFormModel, Photo>(formModel);
                _photoService.Update(model);
                return RedirectToAction("Index");
            }
            return View(formModel);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Delete(int id)
        {
            var model = _photoService.GetById(id);
            if (model != null)
            {
                var viewModel = Mapper.Map<Photo, PhotoViewModel>(model);
                return View(viewModel);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = _photoService.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            //TODO:Kireev. реализовать в бд каскадное удаление для фотографий альбома
            _photoService.Delete(model);
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