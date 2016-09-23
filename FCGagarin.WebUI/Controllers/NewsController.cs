using AutoMapper;
using FCGagarin.DAL.Concrete;
using FCGagarin.Domain.Model;
using FCGagarin.WebUI.Helpers;
using FCGagarin.WebUI.ViewModels;
using FCGagarin.WebUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FCGagarin.WebUI.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        #region News Новости

        public ActionResult News()
        {
            using (var db = new FCGagarinContext())
            {
                var all_news = db.News.Include(x=>x.Author).ToList().OrderByDescending(x=>x.CreateDate);
                var model = Mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(all_news);
                foreach (var item in model)
                {
                    item.Text = LinkPrepare.RawTextToDB(item.Text);
                }
                return View(model);
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        public ActionResult AddNews()
        {
            return View();
        }

        [Authorize]//TODO: заменить на проверку роли
        [HttpPost]
        public ActionResult AddNews(NewsFormModel formModel)
        {
            formModel.AuthorId = User.Identity.GetUserProfile().Id;
            if (ModelState.IsValid)
            {
                var newNews = Mapper.Map<NewsFormModel, News>(formModel);
                using (var db = new FCGagarinContext())
                {
                    db.News.Add(newNews);
                    db.SaveChanges();
                }
                return RedirectToAction("News");
            }

            return View(formModel);
        }

        [Authorize]//TODO: заменить на проверку роли
        public ActionResult EditNews(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.News.Find(id);
                if (model != null)
                {
                    var formModel = Mapper.Map<News, NewsFormModel>(model);
                    return View(formModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        [HttpPost]
        public ActionResult EditNews(NewsFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<NewsFormModel, News>(formModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("News");
                }
            }
            else
            {
                return View(formModel);
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        public ActionResult DeleteNews(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.News.Include(x=>x.Author)
                    .FirstOrDefault(x=>x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<News, NewsViewModel>(model);
                    viewModel.Text = LinkPrepare.RawTextToDB(viewModel.Text);
                    return View(viewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        [HttpPost]
        [ActionName("DeleteNews")]
        public ActionResult DeleteNewsConfirmed(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var news = db.News.Find(id);
                if (news == null)
                {
                    return HttpNotFound();
                }
                db.News.Remove(news);
                db.SaveChanges();
                return RedirectToAction("News");
            }
        }
        #endregion

        #region Announcement Анонсы
        public ActionResult Announcements()
        {
            using (var db = new FCGagarinContext())
            {
                var all_announcements = db.Announcements.Include(x=>x.Author);
                var model = Mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementViewModel>>(all_announcements);
                foreach (var item in model)
                {
                    item.Text = LinkPrepare.RawTextToDB(item.Text);
                }
                return View(model);
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        public ActionResult AddAnnouncement()
        {
            return View();
        }

        [Authorize]//TODO: заменить на проверку роли
        [HttpPost]
        public ActionResult AddAnnouncement(AnnouncementFormModel formModel)
        {
            formModel.AuthorId = User.Identity.GetUserProfile().Id;
            if (ModelState.IsValid)
            {
                var newAnnouncement = Mapper.Map<AnnouncementFormModel, Announcement>(formModel);
                using (var db = new FCGagarinContext())
                {
                    db.Announcements.Add(newAnnouncement);
                    db.SaveChanges();
                }
                return RedirectToAction("Announcements");
            }

            return View(formModel);
        }

        [Authorize]//TODO: заменить на проверку роли
        public ActionResult EditAnnouncement(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Announcements.Find(id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<Announcement, AnnouncementFormModel>(model);
                    return View(viewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        [HttpPost]
        public ActionResult EditAnnouncement(AnnouncementFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<AnnouncementFormModel, Announcement>(formModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Announcements");
                }
            }
            else
            {
                return View(formModel);
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        public ActionResult DeleteAnnouncement(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Announcements.Include(x=>x.Author)
                    .FirstOrDefault(x=>x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<Announcement, AnnouncementViewModel>(model);
                    viewModel.Text = LinkPrepare.RawTextToDB(viewModel.Text);
                    return View(viewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [Authorize]//TODO: заменить на проверку роли
        [HttpPost]
        [ActionName("DeleteAnnouncement")]
        public ActionResult DeleteAnnouncementConfirmed(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var announsement = db.Announcements.Find(id);
                if (announsement == null)
                {
                    return HttpNotFound();
                }
                db.Announcements.Remove(announsement);
                db.SaveChanges();
                return RedirectToAction("Announcements");
            }
        }
        #endregion
    }
}