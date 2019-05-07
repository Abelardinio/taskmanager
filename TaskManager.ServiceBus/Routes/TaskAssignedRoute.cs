namespace TaskManager.ServiceBus.Routes
{
    public class TaskAssignedRoute : IRoute
    {
        public EventLookup Event => EventLookup.TaskAssigned;
    }
}