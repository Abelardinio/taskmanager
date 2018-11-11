namespace TaskManager.MessagingService
{
    public interface IHubContextAccessor
    {
        IHubClient<TasksHub> TasksHubContext { get; }
    }
}