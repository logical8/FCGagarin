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
        public ActionResult Index()
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

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult AddNews()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Moderator")]
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
                return RedirectToAction("Index");
            }

            return View(formModel);
        }

        [Authorize(Roles = "Admin, Moderator")]
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

        [Authorize(Roles = "Admin, Moderator")]
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
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(formModel);
            }
        }

        [Authorize(Roles = "Admin, Moderator")]
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
        [Authorize(Roles = "Admin, Moderator")]
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
                return RedirectToAction("Index");
            }
        }
    }
}