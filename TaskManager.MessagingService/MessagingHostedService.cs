using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using TaskManager.Core;
using TaskManager.ServiceBus;

namespace TaskManager.MessagingService
{
    public class MessagingHostedService : IHostedService
    {
        private readonly IDependencyResolver _resolver;
        private IConnectionStorage _connectionStorage;
        private List<IMessagingService> _messagingServices;
        private IConnectionFactory _connectionFactory;

        public MessagingHostedService(IHubContext<TasksHub> tasksHubContext, IDependencyResolver resolver)
        {
            _resolver = resolver;
            HubContext.SetTasksHubContext(tasksHubContext);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var tuple = _resolver.Resolve<Tuple<IConnectionFactory, IConnectionStorage, List<IMessagingService>>>();

            _connectionFactory = tuple.Item1;
            _connectionStorage = tuple.Item2;
            _messagingServices = tuple.Item3;


            _connectionFactory.Create();
            _messagingServices.ForEach(x => x.Start());

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messagingServices.ForEach( x=>x.Dispose());
            _connectionStorage.Get().Dispose();
            return Task.CompletedTask;
        }
    }
}