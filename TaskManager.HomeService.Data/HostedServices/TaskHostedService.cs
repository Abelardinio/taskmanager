using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.HomeService.Data.HostedServices
{
    public class TaskHostedService : IHostedService
    {
        private readonly IEventConnectionContext _connectionContext;
        private readonly ITaskEventAccessor _taskEventAccessor;
        private readonly IUserTasksDataAccessor _userTasksDataAccessor;
        private IEventScope _eventScope;

        public TaskHostedService(
            IEventConnectionContext connectionContext,
            ITaskEventAccessor taskEventAccessor,
            IUserTasksDataAccessor userTasksDataAccessor)
        {
            _connectionContext = connectionContext;
            _taskEventAccessor = taskEventAccessor;
            _userTasksDataAccessor = userTasksDataAccessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventScope = _connectionContext.EventScope();
            _taskEventAccessor.OnStatusUpdated(OnStatusUpdated);
            _taskEventAccessor.OnTaskAssigned(OnTaskAssigned);
            _taskEventAccessor.OnTaskUnassigned(OnTaskUnassigned);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventScope?.Dispose();
            return Task.CompletedTask;
        }

        public void OnStatusUpdated(ITaskStatusUpdatedMessage message)
        {
            if (message.AssignedUserId.HasValue)
            {
                if (message.Status == TaskStatus.Removed)
                {
                    _userTasksDataAccessor.RemoveTask(message.AssignedUserId.Value, message.TaskId);
                }
                else
                {
                    _userTasksDataAccessor.ChangeTaskStatus(message.AssignedUserId.Value, message.TaskId, message.Status);
                }
            }
        }

        public void OnTaskAssigned(ITaskAssignedMessage message)
        {
            _userTasksDataAccessor.AddTaskToUser(message.AssignedUserId, message.TaskId, message.Status, message);
        }

        public void OnTaskUnassigned(ITaskUnassignedMessage message)
        {
            _userTasksDataAccessor.RemoveTask(message.UserId, message.TaskId);
        }
    }
}