namespace TaskManager.ServiceBus.Exchanges
{
    public class TaskExchange : IExchange
    {
        public ExchangeLookup Exchange => ExchangeLookup.Task;
    }
}
