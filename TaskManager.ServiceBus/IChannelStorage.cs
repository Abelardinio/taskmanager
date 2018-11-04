using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public interface IChannelStorage
    {
        IModel Get();
    }
}