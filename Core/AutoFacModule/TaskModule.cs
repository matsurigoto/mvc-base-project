using System.Reflection;
using Autofac;
using Core.Common.Task;

namespace Core.AutofacModule
{
    /// <summary>
    /// Autofac用來註冊Task相關的服務
    /// </summary>
    public class TaskModule : Autofac.Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            var assemblies = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assemblies).As<IRunAfterEachRequest>().InstancePerRequest();
            builder.RegisterAssemblyTypes(assemblies).As<IRunAtStartup>();
            builder.RegisterAssemblyTypes(assemblies).As<IRunOnEachRequest>().InstancePerRequest();
            builder.RegisterAssemblyTypes(assemblies).As<IRunOnError>();
        }
    }
}
