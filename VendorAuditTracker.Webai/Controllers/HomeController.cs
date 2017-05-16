using System.Web.Mvc;

namespace VendorAuditTracker.Webai.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
