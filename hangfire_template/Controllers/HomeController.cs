using System.Web.Mvc;

namespace hangfire_template.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() 
        {
            return View();
        }
    }
}