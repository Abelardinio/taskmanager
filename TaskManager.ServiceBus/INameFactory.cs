namespace TaskManager.ServiceBus
{
    public interface INameFactory
    {
        string GetRoutingKey(EventLookup e);
        string GetQueue(string applicationName, EventLookup e);
        string GetExchange(ExchangeLookup exchange);
    }
}