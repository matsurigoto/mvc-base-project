using System.Web.Mvc;
using Core.Base;
using Core.Common.Security;

namespace BaseProject.Admin.Controllers
{
    public class HomeController : CoreBaseController
    {
        public IResourceOperationCollection ResourceCollection { get; set; }

        public HomeController()
        {

        }

        public HomeController(IResourceOperationCollection resourceCollection)
        {
            ResourceCollection = resourceCollection;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}