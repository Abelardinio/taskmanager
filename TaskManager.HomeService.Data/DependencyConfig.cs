using Microsoft.Extensions.Hosting;
using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.Common.Data.AppSettings;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskManager.HomeService.Data.DataProviders;
using TaskManager.HomeService.Data.HostedServices;
using TaskManager.HomeService.DbConnection;
using TaskManager.HomeService.DbConnection.DataAccessors;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;
using TaskManager.ServiceBus.Routes;

namespace TaskManager.HomeService.Data
{
    public static class DependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IRouteSettings, IServiceBusClientSettings>().To<ServiceBusRouteSettings>().InSingletonScope();
            kernel.Bind<IConnectionSettings, IDbConnectionSettings>().To<AppSettings>().InSingletonScope();
            kernel.Bind<ITaskEventAccessor>().To<TaskEventAccessor>();
            kernel.Bind<IContext>().To<Context>().InSingletonScope();
            kernel.Bind<IRoute>().To<TaskStatusUpdatedRoute>();
            kernel.Bind<IRoute>().To<TaskAssignedRoute>();
            kernel.Bind<IRoute>().To<TaskUnassignedRoute>();
            kernel.Bind<IUserTasksDataAccessor>().To<UserTasksDataAccessor>();
            kernel.Bind<IUserTasksDataProvider>().To<UserTasksDataProvider>();
            kernel.Bind<IEventConnectionContext>().To<ConnectionContext>().InCallScope();
            kernel.Bind<IHostedService>().To<TaskHostedService>();
        }
    }
}
