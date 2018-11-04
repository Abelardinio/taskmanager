using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public interface IConnectionFactory
    {
        void Create();
    }
}