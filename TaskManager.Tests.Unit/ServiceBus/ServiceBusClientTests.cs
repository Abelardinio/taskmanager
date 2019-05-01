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
            var nameFactoryMock = new Mock<INameFactory>();
            var serviceBusClientSettingsMock = new Mock<IServiceBusClientSettings>();
            var storageMock = new Mock<IChannelStorage>();
            var channelMock = new Mock<IModel>();
            storageMock.Setup(x => x.Get()).Returns(channelMock.Object);
            _client = new ServiceBusClient(_serializerMock.Object, storageMock.Object, nameFactoryMock.Object,
                serviceBusClientSettingsMock.Object);
        }


        [Fact]
        public void SendMethodShouldSerializeObjectBeforeSendTest()
        {
            var sentObject = new object();
            _client.SendMessage(EventLookup.TaskStatusUpdated, sentObject);
            _serializerMock.Verify(x => x.Serialize(sentObject), Times.Once);
        }
    }
}