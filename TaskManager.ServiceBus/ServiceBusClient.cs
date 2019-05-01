using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TaskManager.ServiceBus
{
    public class ServiceBusClient : IServiceBusClient
    {
        private readonly IMessageSerializer _serializer;
        private readonly IChannelStorage _storage;
        private readonly INameFactory _nameFactory;
        private readonly IServiceBusClientSettings _serviceBusClientSettings;

        public ServiceBusClient(
            IMessageSerializer serializer,
            IChannelStorage storage,
            INameFactory nameFactory,
            IServiceBusClientSettings serviceBusClientSettings)
        {
            _serializer = serializer;
            _storage = storage;
            _nameFactory = nameFactory;
            _serviceBusClientSettings = serviceBusClientSettings;
        }

        public void SendMessage(EventLookup e, object obj)
        {
            _storage.Get().BasicPublish(exchange: _nameFactory.GetExchange(EventExchangeMapping.Dictionary[e]),
                routingKey: _nameFactory.GetRoutingKey(e),
                basicProperties: null,
                body: _serializer.Serialize(obj));
        }

        public void Subscribe<T>(EventLookup e, Action<T> handler)
        {
            var channel = _storage.Get();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                handler((T)_serializer.Deserialize(ea.Body));
            };
            channel.BasicConsume(queue: _nameFactory.GetQueue(_serviceBusClientSettings.ApplicationName, e),
                autoAck: true,
                consumer: consumer);
        }
    }
}