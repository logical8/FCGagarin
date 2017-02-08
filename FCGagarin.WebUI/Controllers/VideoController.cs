using AutoMapper;
using FCGagarin.WebUI.Extensions;
using FCGagarin.WebUI.ViewModels;
using System.Web.Mvc;

namespace FCGagarin.WebUI.Controllers
{
    public class VideoController : Controller
    {
        //public ActionResult Index(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    using (var db = new FCGagarinContext())
        //    {
        //        var videos = db.Videos
        //            .Where(v => v.AlbumId == id);
        //        var model = Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(videos);

        //        return View(model);
        //    }
        //}

        [Authorize(Roles = "Moderator")]
        public ActionResult Create(int? albumId)
        {
            using (var db = new FCGagarinContext())
            {
                var album = db.VideoAlbums
                    .FirstOrDefault(v => v.Id == albumId);
                if (albumId == null)
                {
                    return HttpNotFound();
                }
                return View(new VideoFormModel { AlbumId = albumId.GetValueOrDefault(), AlbumName = album.Name });
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Create(VideoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                var newVideo = Mapper.Map<VideoFormModel, Video>(formModel);
                newVideo.AuthorId = User.Identity.GetUserProfile().Id;
                using (var db = new FCGagarinContext())
                {
                    db.Videos.Add(newVideo);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "VideoAlbum", new { id = newVideo.AlbumId});
            }

            return View(formModel);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Videos.Find(id);
                if (model != null)
                {
                    var formModel = Mapper.Map<Video, VideoFormModel>(model);
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
        public ActionResult Edit(VideoFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<VideoFormModel, Video>(formModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "VideoAlbum", new { id = model.AlbumId });
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
                var model = db.Videos
                    .Include("Author")
                    .FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<Video, VideoViewModel>(model);
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
                var video = db.Videos.Find(id);
                if (video == null)
                {
                    return HttpNotFound();
                }
                db.Videos.Remove(video);
                db.SaveChanges();
                return RedirectToAction("Details", "VideoAlbum", new { id = video.AlbumId });
            }
        }
    }
}