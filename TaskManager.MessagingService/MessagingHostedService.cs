using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Ninject;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.ServiceBus;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.MessagingService
{
    public class MessagingHostedService : IHostedService
    {
        private readonly IHubContext<TasksHub> _tasksHubContext;
        private readonly IKernel _kernel;

        public MessagingHostedService(IKernel kernel, IHubContext<TasksHub> tasksHubContext)
        {
            _tasksHubContext = tasksHubContext;
            _kernel = kernel;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _kernel.Get<IConnectionFactory>().Create();
            _kernel.Get<IEventScopeFactory>().Create();
            var taskEventAccessor = _kernel.Get<ITaskEventAccessor>();
            taskEventAccessor.OnStatusUpdated(OnStatusUpdated);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _kernel.Get<IConnectionStorage>().Get().Dispose();
            _kernel.Get<IChannelStorage>().Get().Dispose();
            return Task.CompletedTask;
        }

        public void OnStatusUpdated(ITaskStatusUpdatedMessage message)
        {
            switch (message.Status)
            {
                case Core.TaskStatus.Completed:
                    _tasksHubContext.Clients.All.SendAsync("TASK_COMPLETED", message.TaskId);
                    break;
                case TaskStatus.Removed:
                    _tasksHubContext.Clients.All.SendAsync("TASK_DELETED", message.TaskId);
                    break;
            }
        }
    }
}