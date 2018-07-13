using Ninject;
using Ninject.Web.Common;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.DbConnection;
using TaskManager.DbConnection.DataAccessors;

namespace TaskManager.Data
{
    public static class DependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<ITaskDataAccessor>().To<TaskDataAccessor>();
            kernel.Bind<IConnectionContext, IContextFactory>().To<ConnectionContext>().InRequestScope();
        }
    }
}