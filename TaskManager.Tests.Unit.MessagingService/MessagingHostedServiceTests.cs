using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Moq;
using RabbitMQ.Client;
using TaskManager.Core;
using TaskManager.MessagingService;
using TaskManager.ServiceBus;
using Xunit;
using IConnectionFactory = TaskManager.ServiceBus.IConnectionFactory;

namespace TaskManager.Tests.Unit.MessagingService
{
    public class MessagingHostedServiceTests
    {
        private readonly Mock<IDependencyResolver> _dependencyResolverMock;
        private readonly Mock<IConnectionFactory> _connectionFactoryMock;
        private readonly Mock<IConnectionStorage> _connectionStorageMock;
        private readonly List<IMessagingService> _messagingServices;
        private readonly Mock<IMessagingService> _firstMessagingServiceMock;
        private readonly Mock<IMessagingService> _secondMessagingServiceMock;
        private readonly Mock<IConnection> _connectionMock;
        private readonly IHostedService _hostedService;

        public MessagingHostedServiceTests()
        {
            _connectionFactoryMock = new Mock<IConnectionFactory>();
            _dependencyResolverMock = new Mock<IDependencyResolver>();
            _connectionStorageMock = new Mock<IConnectionStorage>();
            _firstMessagingServiceMock = new Mock<IMessagingService>();
            _secondMessagingServiceMock = new Mock<IMessagingService>();
            _connectionMock =  new Mock<IConnection>();
            _messagingServices = new List<IMessagingService>()
            {
                _firstMessagingServiceMock.Object,
                _secondMessagingServiceMock.Object
            };


            _connectionStorageMock.Setup(x => x.Get()).Returns(_connectionMock.Object);

            var resolvedObject = Tuple.Create(
                _connectionFactoryMock.Object,
                _connectionStorageMock.Object,
                _messagingServices);
            _dependencyResolverMock
                .Setup(x => x.Resolve<Tuple<IConnectionFactory, IConnectionStorage, List<IMessagingService>>>())
                .Returns(resolvedObject);

            _hostedService = new MessagingHostedService(null,_dependencyResolverMock.Object);
        }

        [Fact]
        public void StartAsyncSuccessMethodTest()
        {
            _hostedService.StartAsync(CancellationToken.None).Wait();
            _connectionFactoryMock.Verify(x=>x.Create(), Times.Once);
            _firstMessagingServiceMock.Verify(x=>x.Start(),Times.Once);
            _secondMessagingServiceMock.Verify(x => x.Start(), Times.Once);
        }

        [Fact]
        public void StopAsyncSuccessMethodTest()
        {
            _hostedService.StartAsync(CancellationToken.None).Wait();
            _hostedService.StopAsync(CancellationToken.None).Wait();
            _connectionStorageMock.Verify(x => x.Get(), Times.Once);
            _connectionMock.Verify(x=>x.Dispose(), Times.Once);
            _firstMessagingServiceMock.Verify(x => x.Dispose(), Times.Once);
            _secondMessagingServiceMock.Verify(x => x.Dispose(), Times.Once);
        }
    }
}