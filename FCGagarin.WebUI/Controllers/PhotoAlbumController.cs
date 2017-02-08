using AutoMapper;
using FCGagarin.WebUI.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FCGagarin.WebUI.Controllers
{
    public class PhotoAlbumController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new FCGagarinContext())
            {
                var allAlbums = db.PhotoAlbums
                    .Include("Photos");
                var model = Mapper.Map<IEnumerable<PhotoAlbum>, IEnumerable<PhotoAlbumViewModel>>(allAlbums);

                return View(model);
            }
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
                using (var db = new FCGagarinContext())
                {
                    db.PhotoAlbums.Add(newAlbum);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(formModel);
        }

        public ActionResult Details(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var album = db.PhotoAlbums
                    .Include("Photos")
                    .Include("Photos.Author")
                    .FirstOrDefault(pa => pa.Id == id);
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

        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.PhotoAlbums.Find(id);
                if (model != null)
                {
                    var formModel = Mapper.Map<PhotoAlbum, PhotoAlbumFormModel>(model);
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
        public ActionResult Edit(PhotoAlbumFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<PhotoAlbumFormModel, PhotoAlbum>(formModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
                var model = db.PhotoAlbums
                    .Include("Photos")
                    .Include("Photos.Author")
                    .FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<PhotoAlbum, PhotoAlbumViewModel>(model);
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
                var model = db.PhotoAlbums.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                var photos = db.Photos
                    .Where(v => v.AlbumId == id)
                    .AsEnumerable();
                db.Photos.RemoveRange(photos);
                db.PhotoAlbums.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}