using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public interface IConnectionStorage
    {
        IConnection Get();
    }
}