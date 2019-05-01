using System;

namespace TaskManager.ServiceBus
{
    public interface IServiceBusClient
    {
        void SendMessage(EventLookup e, object obj);

        void Subscribe<T>(EventLookup e, Action<T> handler);
    }
}