using Microsoft.Extensions.Hosting;
using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.Common.Data;
using TaskManager.Common.Data.AppSettings;
using TaskManager.Common.Data.HostedServices;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.EventAccessors;
using TaskManager.MessagingService.DbConnection;
using TaskManager.MessagingService.DbConnection.DataAccessots;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;
using TaskManager.ServiceBus.Routes;

namespace TaskManager.MessagingService.Data
{
    public static class DependencyConfig
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IConnectionSettings, IDbConnectionSettings>().To<AppSettings>();
            kernel.Bind<IPermissionsEventAccessor>().To<PermissionsEventAccessor>();
            kernel.Bind<IPermissionsDataAccessor>().To<PermissionsDataAccessor>();
            kernel.Bind<ITaskEventAccessor>().To<TaskEventAccessor>();
            kernel.Bind<IDependencyResolver>().To<DependencyResolver>().InSingletonScope();
            kernel.Bind<IEventConnectionContext>().To<ConnectionContext>();
            kernel.Bind<IConnectionScopeFactory, IContextStorage>().To<ContextFactory>().InCallScope();
            kernel.Bind<IConnectionContext>().To<ConnectionContext>();
            kernel.Bind<IRoute>().To<TaskStatusUpdatedRoute>().InSingletonScope();
            kernel.Bind<IRoute>().To<PermissionsUpdatedRoute>().InSingletonScope();
            kernel.Bind<IHostedService>().To<PermissionsHostedService>();
            kernel.Bind<IRouteSettings, IServiceBusClientSettings>().To<ServiceBusRouteSettings>().InSingletonScope();
            kernel.Bind(typeof(IHubClient<>)).To(typeof(HubClient<>));
        }
    }
}