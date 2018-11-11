using System;
using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public class ServiceBusConnectionFactory : IConnectionFactory, IConnectionStorage
    {
        private IConnection _connection;
        private readonly IRabbitMqConnectionFactory _connectionFactory;

        public ServiceBusConnectionFactory(IRabbitMqConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Create()
        {
            _connection = _connectionFactory.CreateConnection();

            DeclareQueues();
        }

        public IConnection Get()
        {
            return _connection;
        }

        private void DeclareQueues()
        {
            using (var channel = _connection.CreateModel())
            {
                foreach (var queueNumber in (QueueNumber[])Enum.GetValues(typeof(QueueNumber)))
                {
                    channel.QueueDeclare(queue: queueNumber.ToString(),
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                }
            }
        }
    }
}