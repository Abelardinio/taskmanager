using System;
using Moq;
using Xunit;
using RabbitMQ.Client;
using TaskManager.ServiceBus;
using IConnectionFactory = TaskManager.ServiceBus.IConnectionFactory;

namespace TaskManager.Tests.Unit.ServiceBus
{
    public class ServiceBusConnectionFactoryTests
    {
        private readonly Mock<IRabbitMqConnectionFactory> _rabbitMqFactoryMock;
        private readonly Mock<IConnection> _connectionMock;
        private readonly Mock<IModel> _channelMock;
        private readonly IConnectionFactory _connectionFactory;

        public ServiceBusConnectionFactoryTests()
        {
            _rabbitMqFactoryMock = new Mock<IRabbitMqConnectionFactory>();
            _connectionMock = new Mock<IConnection>();
            _channelMock =  new Mock<IModel>();
            _rabbitMqFactoryMock.Setup(x => x.CreateConnection()).Returns(_connectionMock.Object);
            _connectionMock.Setup(x => x.CreateModel()).Returns(_channelMock.Object);
            _connectionFactory = new ServiceBusConnectionFactory(_rabbitMqFactoryMock.Object);
        }

        [Fact]
        public void QueuesAreDeclaredOnConnectionCreateTest()
        {
            _connectionFactory.Create();
            _rabbitMqFactoryMock.Verify(x => x.CreateConnection(), Times.Once);
            _connectionMock.Verify(x => x.CreateModel(), Times.Once);
            _channelMock.Verify(x => x.Dispose(), Times.Once);

            foreach (var queueNumber in (QueueNumber[])Enum.GetValues(typeof(QueueNumber)))
            {
                _channelMock.Verify(x => x.QueueDeclare(queueNumber.ToString(),
                    false,
                    false,
                    false,
                    null), Times.Once);
            }
        }
    }
}