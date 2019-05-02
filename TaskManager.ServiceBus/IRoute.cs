namespace TaskManager.ServiceBus
{
    public interface IRoute
    {
        EventLookup Event { get; }
    }
}