using System;
using Ninject;
using Ninject.Activation;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;

namespace TaskManager.MessagingService
{
    public static class DependencyConfig
    {
        public static void Configure(IKernel kernel, Func<IContext,object> requestScope)
        {
            kernel.Bind<IConnectionSettings>().To<AppSettings.AppSettings>();
            kernel.Bind<ITaskEventAccessor>().To<TaskEventAccessor>();
            kernel.Bind<IChannelStorage, IEventScopeFactory>().To<ChannelFactory>().InSingletonScope();
            kernel.Bind<IConnectionStorage, IConnectionFactory>().To<ServiceBusConnectionFactory>().InSingletonScope();
            kernel.Bind<IServiceBusClient>().To<ServiceBusClient>();
            kernel.Bind<IMessageSerializer>().To<MessageSerializer>();
        }
    }
}