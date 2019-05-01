namespace TaskManager.ServiceBus.Routes
{
    public class TaskStatusUpdatedRoute : IRoute
    {
        public ExchangeLookup Exchange => ExchangeLookup.Task;
        public EventLookup Event => EventLookup.TaskStatusUpdated;
    }
}
