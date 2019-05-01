namespace TaskManager.ServiceBus
{
    public interface IRoute
    {
        ExchangeLookup Exchange { get; }
        EventLookup Event { get; }
    }
}