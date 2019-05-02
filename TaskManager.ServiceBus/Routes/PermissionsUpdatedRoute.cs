namespace TaskManager.ServiceBus.Routes
{
    public class PermissionsUpdatedRoute : IRoute
    {
        public EventLookup Event => EventLookup.PermissionsUpdated;
    }
}