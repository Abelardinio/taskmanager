using System;
using Microsoft.AspNetCore.SignalR;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;

namespace TaskManager.MessagingService.MessagingServices
{
    public class TasksMessagingService : IMessagingService
    {
        private readonly IEventConnectionContext _context;
        private readonly ITaskEventAccessor _accessor;
        private readonly IHubContext<TasksHub> _tasksHubContext;

        private IEventScope _eventScope;

        public TasksMessagingService(IDependencyResolver resolver)
        {
            var tuple = resolver.Resolve<Tuple<IEventConnectionContext, ITaskEventAccessor, IHubContextAccessor>>();
            _context = tuple.Item1;
            _accessor = tuple.Item2;
            _tasksHubContext = tuple.Item3.TasksHubContext;
        }

        public void Start()
        {
            _eventScope = _context.EventScope();
            _accessor.OnStatusUpdated(OnStatusUpdated);
        }

        public void Dispose()
        {
            _eventScope.Dispose();
        }

        public void OnStatusUpdated(ITaskStatusUpdatedMessage message)
        {
            switch (message.Status)
            {
                case TaskStatus.Completed:
                    _tasksHubContext.Clients.All.SendAsync("TASK_COMPLETED", message.TaskId);
                    break;
                case TaskStatus.Removed:
                    _tasksHubContext.Clients.All.SendAsync("TASK_DELETED", message.TaskId);
                    break;
            }
        }
    }
}