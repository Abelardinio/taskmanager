namespace TaskManager.ServiceBus.Routes
{
    public class TaskStatusUpdatedRoute : IRoute
    {
        public EventLookup Event => EventLookup.TaskStatusUpdated;
    }
}
