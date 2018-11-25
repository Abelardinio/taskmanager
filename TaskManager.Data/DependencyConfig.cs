using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskManager.Data.DataProviders;
using TaskManager.DbConnection;
using TaskManager.DbConnection.DataAccessors;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;

namespace TaskManager.Data
{
    public static class DependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<ITaskDataAccessor>().To<TaskDataAccessor>();
            kernel.Bind<IConnectionScopeFactory, IContextStorage>().To<ContextFactory>().InCallScope();
            kernel.Bind<IConnectionContext>().To<ConnectionContext>();
            kernel.Bind<ITaskEventAccessor>().To<TaskEventAccessor>();
            kernel.Bind<IChannelStorage, IEventScopeFactory>().To<ChannelFactory>().InCallScope();
            kernel.Bind<IConnectionStorage, IConnectionFactory>().To<ServiceBusConnectionFactory>().InSingletonScope();
            kernel.Bind<IServiceBusClient>().To<ServiceBusClient>();
            kernel.Bind<IMessageSerializer>().To<MessageSerializer>();
            kernel.Bind<IRabbitMqConnectionFactory>().To<RabbitMqConnectionFactory>();
            kernel.Bind<ITaskDataProvider>().To<TaskDataProvider>();
            kernel.Bind<IConnectionSettings>().To<AppSettings.AppSettings>();
        }
    }
}