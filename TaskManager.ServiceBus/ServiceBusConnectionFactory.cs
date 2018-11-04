using System;
using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public class ServiceBusConnectionFactory : IConnectionFactory, IConnectionStorage
    {
        private IConnection _connection;
        private readonly IConnectionSettings _settings;

        public ServiceBusConnectionFactory(IConnectionSettings settings)
        {
            _settings = settings;
        }

        public void Create()
        {
            _connection = new ConnectionFactory
            {
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost,
                HostName = _settings.HostName,
                Port = _settings.Port
            }.CreateConnection();

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