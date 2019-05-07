namespace TaskManager.ServiceBus.Routes
{
    public class TaskUnassignedRoute : IRoute
    {
        public EventLookup Event => EventLookup.TaskUnassigned;
    }
}