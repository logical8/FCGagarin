using System.Web.Mvc;
using FCGagarin.BLL.Services.Interfaces;

namespace FCGagarin.PL.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImportService _importService;

        public HomeController(IImportService importService)
        {
            _importService = importService;
        }

        public ActionResult Index()
        {
            _importService.ImportRounds(915, "2017-2018");
            return View();
        }
    }
}