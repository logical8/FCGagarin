﻿using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FCGagarin.DAL.EF;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;
using FCGagarin.PL.WebUI.Extensions;
using FCGagarin.PL.WebUI.Helpers;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private const int PageSize = 10; 
        public ActionResult Index(int? id)
        {
            var page = id ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_News", GetNewsPage(page));
            }
            return View(GetNewsPage(page));
        }

        private IEnumerable<NewsViewModel> GetNewsPage(int page = 1)
        {
            var newsToSkip = page * PageSize;
            using (var db = new FCGagarinContext())
            {
                var allNews = db.News.Include(x => x.Author).ToList().OrderByDescending(x => x.CreateDate);
                var model = Mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(allNews);
                foreach (var item in model)
                {
                    item.Text = LinkPrepare.RawTextToDb(item.Text);
                }
                return model.Skip(newsToSkip).Take(PageSize);
            }
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Create(NewsFormModel formModel)
        {
            var relativePath = "~/Data/uploads/images_news";
            var directory = Server.MapPath(relativePath);

            formModel.AuthorId = User.Identity.GetUserProfile().Id;
            if (ModelState.IsValid)
            {
                string fileName = null;
                var pathToImage = string.Empty;
                if (formModel != null && formModel.Image != null && formModel.Image.ContentLength > 0)
                {
                    fileName = Path.GetFileName(formModel.Image.FileName);
                    pathToImage = Path.Combine(directory, fileName);
                    formModel.Image.SaveAs(pathToImage);
                }
                var newNews = Mapper.Map<NewsFormModel, News>(formModel);
                newNews.PathToImage = fileName != null ? Path.Combine(relativePath, fileName) : fileName;
                using (var db = new FCGagarinContext())
                {
                    db.News.Add(newNews);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(formModel);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Edit(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.News.Find(id);
                if (model != null)
                {
                    var formModel = Mapper.Map<News, NewsFormModel>(model);
                    return View(formModel);
                }
                return HttpNotFound();
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public ActionResult Edit(NewsFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<NewsFormModel, News>(formModel);
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(formModel);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Delete(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.News.Include(x => x.Author)
                    .FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<News, NewsViewModel>(model);
                    viewModel.Text = LinkPrepare.RawTextToDb(viewModel.Text);
                    return View(viewModel);
                }
                return HttpNotFound();
            }
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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