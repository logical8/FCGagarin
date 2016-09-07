using FCGagarin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        public ActionResult AddAnnouncement()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddAnnouncement(AnnouncementViewModel model)
        {
            return View();
        }
    }
}