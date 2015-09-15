using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using Autofac;
using BaseProject.Admin.Controllers;
using BaseProject.Service;
using BaseProject.Model;
using Core.Business;
using Core.Common.Business;
using Core.Common.Security;
using BaseProject.Admin.DI.Autofac.Modules;
using Core.Common.ValidationError;
using Core.Security;
using Core.Security.Filter;
using Core.Utility.ValidationError;
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

            

            ModelBinders.Binders.Add(typeof(ResourceOperationEnum), new EnumFlagsModelBinder());

            var builder = Core.Utility.AutofacBootstrapper.Bootstrap(new List<Assembly>() { typeof(CodeMainController).Assembly });
            builder.RegisterModule(new MvcSiteMapProviderModule());

            ////database serivce
            builder.RegisterType<BaseProjectEntities>().As<DbContext>().InstancePerRequest();

            ////CRUD serivce
            //builder.RegisterType<HttpFileProcessBusiness>().As<IHttpFileProcessBusiness>().InstancePerRequest();
            builder.RegisterType<DeleteManyToManyProcess>().As<IDeleteManyToManyProcess>().InstancePerRequest();

            //model
            builder.RegisterType<ModelStateDictionary>().As<ModelStateDictionary>().InstancePerRequest();
            builder.RegisterType<ModelStateWrapper>().As<IValidationDictionary>().InstancePerRequest();

            ////controller serivce
            builder.RegisterType<UserOpertationLogService>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<CodeMainService>().As<ICodeMainService>().InstancePerRequest();
            builder.RegisterType<CodeService>().As<ICodeService>().InstancePerRequest();

            //resource serivce
            builder.RegisterType<ResourceOperationCollectionBase>().As<IResourceOperationCollection>().SingleInstance();
            builder.RegisterType<ResourceOperationAuthorisation>().As<ResourceOperationAuthorisation>().SingleInstance();



            Core.Utility.AutofacBootstrapper.SetMvcDependencyResolver(builder.Build());
            // Setup global sitemap loader (required)
            MvcSiteMapProvider.SiteMaps.Loader = Core.Utility.AutofacBootstrapper.Container.Resolve<ISiteMapLoader>();
            Core.Utility.TaskBootStrapper.ExecuteIRunAtStartup();
        }
    }
}
