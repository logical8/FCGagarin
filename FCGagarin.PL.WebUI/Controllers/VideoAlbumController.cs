using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FCGagarin.DAL.EF;
using FCGagarin.PL.WebUI.Helpers;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class VideoAlbumController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new FCGagarinContext())
            {
                var allAlbums = db.VideoAlbums
                    .Include("Videos");
                var model = Mapper.Map<IEnumerable<VideoAlbum>, IEnumerable<VideoAlbumViewModel>>(allAlbums);

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
        public ActionResult Create(VideoAlbumFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var newAlbum = Mapper.Map<VideoAlbumFormModel, VideoAlbum>(formModel);
                using (var db = new FCGagarinContext())
                {
                    db.VideoAlbums.Add(newAlbum);
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
                var album = db.VideoAlbums
                    .Include("Videos")
                    .Include("Videos.Author")
                    .FirstOrDefault(va=>va.Id == id);
                if (album == null)
                {
                    return HttpNotFound();
                }
                var viewModel = Mapper.Map<VideoAlbum, VideoAlbumDetailsViewModel>(album);
                foreach (var item in viewModel.VideoViewModelList)
                {
                    item.AlbumId = id;
                    item.Url = LinkPrepare.YoutubeUrlToEmbedUrl(item.Url);
                }
                return View(viewModel);
            }
            
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.VideoAlbums.Find(id);
                if (model != null)
                {
                    var formModel = Mapper.Map<VideoAlbum, VideoAlbumFormModel>(model);
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
        public ActionResult Edit(VideoAlbumFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<VideoAlbumFormModel, VideoAlbum>(formModel);
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
                var model = db.VideoAlbums
                    .Include("Videos")
                    .Include("Videos.Author")
                    .FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<VideoAlbum, VideoAlbumViewModel>(model);
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
                var model = db.VideoAlbums.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                var videos = db.Videos
                    .Where(v => v.AlbumId == id)
                    .AsEnumerable();
                db.Videos.RemoveRange(videos);
                db.VideoAlbums.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}