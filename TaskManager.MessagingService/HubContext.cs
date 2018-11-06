using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    public class HubContext : IHubContextAccessor
    {
        private static IHubContext<TasksHub> _tasksHubContext;

        public static void SetTasksHubContext(IHubContext<TasksHub> tasksHubContext)
        {
            _tasksHubContext = tasksHubContext;
        }

        public IHubContext<TasksHub> TasksHubContext
        {
            get { return _tasksHubContext; }
        }
    }
}