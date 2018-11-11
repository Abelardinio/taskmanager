using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public class RabbitMqConnectionFactory : IRabbitMqConnectionFactory
    {
        private readonly ConnectionFactory _connectionFactory;
        public RabbitMqConnectionFactory( IConnectionSettings settings)
        {
            _connectionFactory = new ConnectionFactory
            {
                UserName = settings.UserName,
                Password = settings.Password,
                VirtualHost = settings.VirtualHost,
                HostName = settings.HostName,
                Port = settings.Port
            };
        }

        public IConnection CreateConnection()
        {
            return _connectionFactory.CreateConnection();
        }
    }
}