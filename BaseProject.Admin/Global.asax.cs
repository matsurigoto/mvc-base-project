using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using HotSpring.Admin.Controllers;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Loader;

namespace BaseProject.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = Core.Utility.AutofacBootstrapper.Bootstrap(new List<Assembly>() { typeof(CodeMainController).Assembly });
            builder.RegisterModule(new MvcSiteMapProviderModule());
        }
    }
}
