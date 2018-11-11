using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    public class HubContext : IHubContextAccessor
    {
        private static IHubClient<TasksHub> _tasksHubContext;

        public static void SetTasksHubContext(IHubContext<TasksHub> tasksHubContext)
        {
            _tasksHubContext = new HubClient<TasksHub>(tasksHubContext);
        }

        public IHubClient<TasksHub> TasksHubContext
        {
            get { return _tasksHubContext; }
        }
    }
}