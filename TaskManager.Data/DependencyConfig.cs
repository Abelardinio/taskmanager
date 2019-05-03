using Microsoft.Extensions.Hosting;
using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.Common.Data.AppSettings;
using TaskManager.Common.Data;
using TaskManager.Common.Data.HostedServices;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskManager.Data.DataProviders;
using TaskManager.DbConnection;
using TaskManager.DbConnection.DataAccessors;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;
using TaskManager.ServiceBus.Routes;

namespace TaskManager.Data
{
    public static class DependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<ITaskDataAccessor>().To<TaskDataAccessor>();
            kernel.Bind<IProjectsDataAccessor>().To<ProjectsDataAccessor>();
            kernel.Bind<IFeaturesDataAccessor>().To<FeaturesDataAccessor>();
            kernel.Bind<IPermissionsDataAccessor>().To<PermissionsDataAccessor>();
            kernel.Bind<IConnectionScopeFactory, IContextStorage>().To<ContextFactory>().InCallScope();
            kernel.Bind<IConnectionContext>().To<ConnectionContext>();
            kernel.Bind<ITaskEventAccessor>().To<TaskEventAccessor>();
            kernel.Bind<IPermissionsEventAccessor>().To<PermissionsEventAccessor>();
            kernel.Bind<ITaskDataProvider>().To<TaskDataProvider>();
            kernel.Bind<IProjectsDataProvider>().To<ProjectsDataProvider>();
            kernel.Bind<IFeaturesDataProvider>().To<FeaturesDataProvider>();
            kernel.Bind<IHostedService>().To<PermissionsHostedService>();
            kernel.Bind<IRoute>().To<PermissionsUpdatedRoute>();
            kernel.Bind<IRouteSettings, IServiceBusClientSettings>().To<ServiceBusRouteSettings>().InSingletonScope();
            kernel.Bind<IConnectionSettings, IDbConnectionSettings>().To<AppSettings>().InSingletonScope();
        }
    }
}