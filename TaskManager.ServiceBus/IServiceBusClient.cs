using System;

namespace TaskManager.ServiceBus
{
    public interface IServiceBusClient
    {
        void SendMessage(QueueNumber number, object obj);

        void Subscribe<T>(QueueNumber number, Action<T> handler);
    }
}