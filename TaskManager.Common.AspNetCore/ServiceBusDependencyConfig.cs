using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.Core.ConnectionContext;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.Exchanges;

namespace TaskManager.Common.AspNetCore
{
    public static class ServiceBusDependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IChannelStorage, IEventScopeFactory>().To<ChannelFactory>().InCallScope();
            kernel.Bind<IConnectionStorage, IConnectionFactory>().To<ServiceBusConnectionFactory>().InSingletonScope();
            kernel.Bind<IServiceBusClient>().To<ServiceBusClient>();
            kernel.Bind<IMessageSerializer>().To<MessageSerializer>();
            kernel.Bind<IRabbitMqConnectionFactory>().To<RabbitMqConnectionFactory>();
            kernel.Bind<IExchange>().To<TaskExchange>().InSingletonScope();
            kernel.Bind<IExchange>().To<PermissionsExchange>().InSingletonScope();
            kernel.Bind<INameFactory>().To<NameFactory>().InSingletonScope();
            kernel.Bind<IRouteInitializer>().To<RouteInitializer>().InSingletonScope();
        }
    }
}