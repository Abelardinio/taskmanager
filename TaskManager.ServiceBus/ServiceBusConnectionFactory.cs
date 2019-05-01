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
        }

        public IConnection Get()
        {
            return _connection;
        }
    }
}