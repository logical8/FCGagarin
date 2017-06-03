using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;
using FCGagarin.PL.WebUI.Extensions;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoAlbumService _photoAlbumService;

        public PhotoController(IPhotoAlbumService photoAlbumService)
        {
            _photoAlbumService = photoAlbumService;
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Create(int? albumId)
        {
            var album = _photoAlbumService.GetById(albumId.GetValueOrDefault());
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(new PhotoFormModel { AlbumId = albumId.GetValueOrDefault(), AlbumName = album.Name });
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Create(PhotoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var newPhoto = Mapper.Map<PhotoFormModel, Photo>(formModel);
                newPhoto.AuthorId = User.Identity.GetUserProfile().Id;
                using (var db = new FCGagarinContext())
                {
                    db.Photos.Add(newPhoto);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "PhotoAlbum", new { id = newPhoto.AlbumId });
            }

            return View(formModel);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Photos.Find(id);
                if (model != null)
                {
                    var formModel = Mapper.Map<Photo, PhotoFormModel>(model);
                    return View(formModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Edit(PhotoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<PhotoFormModel, Photo>(formModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "PhotoAlbum", new { id = model.AlbumId });
                }
            }
            else
            {
                return View(formModel);
            }
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Delete(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Photos
                    .Include("Author")
                    .FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<Photo, PhotoViewModel>(model);
                    return View(viewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var photo = db.Photos.Find(id);
                if (photo == null)
                {
                    return HttpNotFound();
                }
                db.Photos.Remove(photo);
                db.SaveChanges();
                return RedirectToAction("Details", "PhotoAlbum", new { id = photo.AlbumId });
            }
        }
    }
}