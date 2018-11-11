using System;
using Moq;
using NUnit.Framework;
using RabbitMQ.Client;
using TaskManager.ServiceBus;

namespace TaskManager.Tests.Unit.ServiceBus
{
    [TestFixture]
    public class ServiceBusClientTests
    {
        private Mock<IMessageSerializer> _serializerMock;
        private Mock<IChannelStorage> _storageMock;
        private Mock<IModel> _channelMock;
        private IServiceBusClient _client;

        [SetUp]
        public void SetUp()
        {
            _serializerMock =  new Mock<IMessageSerializer>();
            _storageMock = new Mock<IChannelStorage>();
            _channelMock = new Mock<IModel>();
            _storageMock.Setup(x => x.Get()).Returns(_channelMock.Object);
            _client =  new ServiceBusClient(_serializerMock.Object, _storageMock.Object);
        }

        [Test]
        public void SendMethodShouldSerializeObjectBeforeSendTest()
        {
            var sentObject = new object();
            _client.SendMessage(QueueNumber.TaskStatusUpdated, sentObject);
            _serializerMock.Verify(x => x.Serialize(sentObject), Times.Once);
        }
    }
}