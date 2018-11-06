using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    public interface IHubContextAccessor
    {
        IHubContext<TasksHub> TasksHubContext { get; }
    }
}