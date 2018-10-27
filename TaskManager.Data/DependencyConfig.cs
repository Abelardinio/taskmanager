using Ninject;
using Ninject.Web.Common;
using TaskManager.Core.ConnectionContext;
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
            kernel.Bind<IConnectionScopeFactory, IContextStorage>().To<ContextFactory>().InRequestScope();
            kernel.Bind<IConnectionContext>().To<ConnectionContext>();
        }
    }
}