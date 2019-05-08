using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.Core.UserStorage;
using TaskManager.MessagingService.Data;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.MessagingService.WebApi.MessagingServices
{
    public class TasksMessagingService : IHostedService
    {
        private readonly IEventConnectionContext _context;
        private readonly ITaskEventAccessor _accessor;
        private readonly ITasksHubUsersStorage _storage;
        private readonly IHubClient<TasksHub> _tasksHubClient;

        private IEventScope _eventScope;

        public TasksMessagingService(
            IEventConnectionContext context,
            ITaskEventAccessor accessor,
            IHubClient<TasksHub> tasksHubClient,
            ITasksHubUsersStorage storage)
        {
            _context = context;
            _accessor = accessor;
            _tasksHubClient = tasksHubClient;
            _storage = storage;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventScope = _context.EventScope();
            _accessor.OnStatusUpdated(OnStatusUpdated);
            _accessor.OnTaskAssigned(OnTaskAssigned);
            _accessor.OnTaskUnassigned(OnTaskUnassigned);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventScope?.Dispose();
            return Task.CompletedTask;
        }

        public void OnStatusUpdated(ITaskStatusUpdatedMessage message)
        {
            var connectionIds = _storage.Get(message.ProjectId, message.CreatorId);

            switch (message.Status)
            {
                case TaskStatus.Completed:
                    _tasksHubClient.SendAsync("TASK_COMPLETED", message.TaskId, connectionIds);
                    break;
                case TaskStatus.Removed:
                    _tasksHubClient.SendAsync("TASK_DELETED", message.TaskId, connectionIds);
                    break;
            }
        }

        public void OnTaskAssigned(ITaskAssignedMessage message)
        {
            var connectionIds = _storage.Get(message.AssignedUserId);

            _tasksHubClient.SendAsync("TASK_ASSIGNED", message, connectionIds);
        }

        public void OnTaskUnassigned(ITaskUnassignedMessage message)
        {
            var connectionIds = _storage.Get(message.UserId);

            _tasksHubClient.SendAsync("TASK_UNASSIGNED", message.TaskId, connectionIds);
        }
    }
}