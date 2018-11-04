using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TaskManager.ServiceBus.Messages;

namespace TaskManager.ServiceBus
{
    public class ServiceBusClient : IServiceBusClient
    {
        private readonly IMessageSerializer _serializer;
        private readonly IChannelStorage _storage;

        public ServiceBusClient(IMessageSerializer serializer, IChannelStorage storage)
        {
            _serializer = serializer;
            _storage = storage;
        }

        public void SendMessage(QueueNumber number, object obj)
        {
            _storage.Get().BasicPublish(exchange: "",
                routingKey: number.ToString(),
                basicProperties: null,
                body: _serializer.Serialize(obj));
        }

        public void Subscribe<T>(QueueNumber number, Action<T> handler)
        {
            var channel = _storage.Get();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                handler((T)_serializer.Deserialize(ea.Body));
            };
            channel.BasicConsume(queue: QueueNumber.TaskStatusUpdated.ToString(),
                autoAck: true,
                consumer: consumer);
        }
    }
}