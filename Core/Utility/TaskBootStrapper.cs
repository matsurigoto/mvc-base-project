using System.Collections.Generic;
using System.Linq;
using Autofac;
using Core.Common.Log;
using Core.Common.Task;

namespace Core.Utility
{
    /// <summary>
    /// 把Task執行的方法包起來，方便在Global.asax註冊
    /// </summary>
    public static class TaskBootStrapper
    {
        /// <summary>
        /// 執行是IRunAtStartup有註冊的Service
        /// </summary>
        public static void ExecuteIRunAtStartup()
        {
            var log = AutofacBootstrapper.Container.Resolve<ILog>();
            using (var scope = AutofacBootstrapper.Container.BeginLifetimeScope())
            {
                var runAtStartUpTasks = scope.Resolve<IEnumerable<IRunAtStartup>>();
                log.Debug("{0} instance of run at startup", args: runAtStartUpTasks.Count());
                foreach (var item in runAtStartUpTasks)
                {
                    item.Execute();
                }
            }
        }
    }
}
