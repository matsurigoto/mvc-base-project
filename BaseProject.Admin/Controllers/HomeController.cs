using System.Web.Mvc;
using Core.Base;

namespace BaseProject.Admin.Controllers
{
    public class HomeController : CoreBaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}