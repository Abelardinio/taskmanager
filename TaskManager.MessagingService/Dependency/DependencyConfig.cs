using Microsoft.Extensions.Hosting;
using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.Common.AspNetCore;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.MessagingService.MessagingServices;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;
using TaskManager.ServiceBus.Routes;

namespace TaskManager.MessagingService.Dependency
{
    public static class DependencyConfig
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IConnectionSettings>().To<AppSettings.AppSettings>();
            kernel.Bind<ITaskEventAccessor>().To<TaskEventAccessor>();
            kernel.Bind<IDependencyResolver>().To<DependencyResolver>().InSingletonScope();
            kernel.Bind<IHostedService>().To<TasksMessagingService>();
            kernel.Bind<IEventConnectionContext>().To<ConnectionContext>();
            kernel.Bind<IRoute>().To<TaskStatusUpdatedRoute>().InSingletonScope();
            kernel.Bind<IRouteSettings, IServiceBusClientSettings>().To<ServiceBusRouteSettings>().InSingletonScope();
            kernel.Bind(typeof(IHubClient<>)).To(typeof(HubClient<>));
        }
    }
}