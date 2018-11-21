using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.MessagingService.MessagingServices
{
    public class TasksMessagingService : IHostedService
    {
        private readonly IEventConnectionContext _context;
        private readonly ITaskEventAccessor _accessor;
        private readonly IHubClient<TasksHub> _tasksHubClient;

        private IEventScope _eventScope;

        public TasksMessagingService( IEventConnectionContext context, ITaskEventAccessor accessor, IHubClient<TasksHub> tasksHubClient)
        {
            _context = context;
            _accessor = accessor;
            _tasksHubClient = tasksHubClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventScope = _context.EventScope();
            _accessor.OnStatusUpdated(OnStatusUpdated);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventScope?.Dispose();
            return Task.CompletedTask;
        }

        public void OnStatusUpdated(ITaskStatusUpdatedMessage message)
        {
            switch (message.Status)
            {
                case TaskStatus.Completed:
                    _tasksHubClient.SendAsync("TASK_COMPLETED", message.TaskId);
                    break;
                case TaskStatus.Removed:
                    _tasksHubClient.SendAsync("TASK_DELETED", message.TaskId);
                    break;
            }
        }
    }
}