using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Core.AutofacModule;

namespace Core.Utility
{
    /// <summary>
    /// Autofac註冊Service相關的方法
    /// </summary>
    public static class AutofacBootstrapper
    {
        /// <summary>
        /// 取得Autofac的ContainerBuilder
        /// </summary>
        /// <value>
        /// Autofac的ContainerBuilder
        /// </value>
        public static ContainerBuilder Builder { get; private set; }

        /// <summary>
        /// 取得Autofac的Container
        /// </summary>
        /// <value>
        /// Autofac的Container
        /// </value>
        public static IContainer Container { get; private set; }

        /// <summary>
        /// Autofac最基本的註冊內容
        /// </summary>
        /// <param name="controllerAssemblyList">需要註冊成為Controller的Assembly清單</param>
        /// <param name="regsiterAssemblyTypeList">需要註冊成為IFoo對應到Foo的Assembly清單</param>
        /// <returns>
        /// ContainerBuilder
        /// </returns>
        public static ContainerBuilder Bootstrap(List<Assembly> controllerAssemblyList = null, 
            List<Assembly> regsiterAssemblyTypeList = null)
        {
            Builder = new ContainerBuilder();

            if (controllerAssemblyList == null || controllerAssemblyList.Count == 0)
            {
                controllerAssemblyList = new List<Assembly>()
                {
                    Assembly.GetExecutingAssembly()
                };
            }

            Builder.RegisterControllers(controllerAssemblyList.ToArray());

            Builder.RegisterModule(new AutofacWebTypesModule());

            // 註冊 IFoo 對應到 Foo
             Builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .AsImplementedInterfaces();

             if (regsiterAssemblyTypeList != null && regsiterAssemblyTypeList.Count > 0)
             {
                 Builder.RegisterAssemblyTypes(regsiterAssemblyTypeList.ToArray())
                    .AsImplementedInterfaces();
             }

            // 註冊Foo 對應到 Foo
            // builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            //Builder.RegisterModule<NLogModule>();

            // 取得所有Reference的Library然後註冊Module。
            // var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            // builder.RegisterAssemblyModules(assemblies);

            Builder.RegisterModule<TaskModule>();

            // 讓ActionFilter的Property能夠被注入
            Builder.RegisterFilterProvider();

            Builder.RegisterGeneric(typeof(Core.Service.GenericService<>))
                .As(typeof(Core.Common.Service.IService<>)).InstancePerRequest();

            return Builder;
        }

        /// <summary>
        /// 注入Autofac的Container到Mvc的Dependency Resolver。
        /// </summary>
        /// <param name="container">預設是null。如果是null，就用Builder來BuildContainer，要不然就用傳入的container做IContainer</param>
        public static void SetMvcDependencyResolver(IContainer container = null)
        {
            if (container != null)
            {
                Container = container;
            }
            else
            {
                Container = Builder.Build();
            }

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
