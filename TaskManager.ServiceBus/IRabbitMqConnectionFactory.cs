using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public interface IRabbitMqConnectionFactory
    {
        IConnection CreateConnection();
    }
}