using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public interface IExchange
    {
        ExchangeLookup Exchange { get; }
    }
}