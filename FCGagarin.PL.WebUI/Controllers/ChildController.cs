using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCGagarin.BLL.Presenters.Interfaces;

namespace FCGagarin.PL.WebUI.Controllers
{
    [ChildActionOnly]
    public class ChildController : Controller
    {
        private readonly IRoundPresenter _roundPresenter;

        public ChildController(IRoundPresenter roundPresenter)
        {
            _roundPresenter = roundPresenter;
        }

        public ActionResult ShortResults()
        {
            var model = _roundPresenter.GetNearestRoundsViewModel();
            return View(model);
        }
    }
}