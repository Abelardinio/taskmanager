namespace TaskManager.ServiceBus
{
    public class NameFactory : INameFactory
    {
        public string GetRoutingKey(EventLookup e)
        {
            return e.ToString();
        }

        public string GetQueue(string applicationName, EventLookup e)
        {
            return $"{applicationName}_{e.ToString()}";
        }

        public string GetExchange(ExchangeLookup exchange)
        {
            return exchange.ToString();
        }
    }
}