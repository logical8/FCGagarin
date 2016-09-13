using AutoMapper;
using FCGagarin.DAL.Concrete;
using FCGagarin.Domain.Model;
using FCGagarin.WebUI.Helpers;
using FCGagarin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FCGagarin.WebUI.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Announcements()
        {
            using (var db = new FCGagarinContext())
            {
                var all_announcements = db.Announcements;
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
        public ActionResult AddAnnouncement(AnnouncementViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newAnnouncement = Mapper.Map<AnnouncementViewModel, Announcement>(viewModel);
                using (var db = new FCGagarinContext())
                {
                    db.Announcements.Add(newAnnouncement);
                    db.SaveChanges();
                }
                return RedirectToAction("Announcements");
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult EditAnnouncement(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Announcements.Find(id);
                if (model != null)
                {
                    var viewModel = Mapper.Map<Announcement, AnnouncementViewModel>(model);
                    return View(viewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditAnnouncement(AnnouncementViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<AnnouncementViewModel, Announcement>(viewModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Announcements");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        [Authorize]
        public ActionResult DeleteAnnouncement(int id)
        {
            using (var db = new FCGagarinContext())
            {
                var model = db.Announcements.Find(id);
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

        [Authorize]
        [HttpPost]
        public ActionResult DeleteAnnouncement(AnnouncementViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FCGagarinContext())
                {
                    var model = Mapper.Map<AnnouncementViewModel, Announcement>(viewModel);
                    db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("Announcements");
                }
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}