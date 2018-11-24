using Moq;
using Xunit;
using RabbitMQ.Client;
using TaskManager.ServiceBus;

namespace TaskManager.Tests.Unit.ServiceBus
{
    public class ServiceBusClientTests
    {
        private readonly Mock<IMessageSerializer> _serializerMock;
        private readonly IServiceBusClient _client;

        public ServiceBusClientTests()
        {
            _serializerMock = new Mock<IMessageSerializer>();
            var storageMock = new Mock<IChannelStorage>();
            var channelMock = new Mock<IModel>();
            storageMock.Setup(x => x.Get()).Returns(channelMock.Object);
            _client = new ServiceBusClient(_serializerMock.Object, storageMock.Object);
        }


        [Fact]
        public void SendMethodShouldSerializeObjectBeforeSendTest()
        {
            var sentObject = new object();
            _client.SendMessage(QueueNumber.TaskStatusUpdated, sentObject);
            _serializerMock.Verify(x => x.Serialize(sentObject), Times.Once);
        }
    }
}