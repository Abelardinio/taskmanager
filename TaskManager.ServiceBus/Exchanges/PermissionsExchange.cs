namespace TaskManager.ServiceBus.Exchanges
{
    public class PermissionsExchange : IExchange
    {
        public ExchangeLookup Exchange => ExchangeLookup.Permissions;
    }
}