using Moq;
using Xunit;
using RabbitMQ.Client;
using TaskManager.ServiceBus;

namespace TaskManager.Tests.Unit.ServiceBus
{
    public class RouteInitializerTests
    {
        private const string ApplicationName = "ApplicationName";
        private readonly Mock<IConnection> _connectionMock;
        private readonly Mock<IModel> _channelMock;
        private readonly Mock<IConnectionStorage> _connectionStorageMock;
        private readonly Mock<IRouteSettings> _routeSettingsMock;
        private readonly Mock<INameFactory> _nameFactoryMock;
        private readonly Mock<IExchange> _firstExchangeMock;
        private readonly Mock<IExchange> _secondExchangeMock;
        private readonly IRouteInitializer _routeInitializer;

        public RouteInitializerTests()
        {
            _connectionMock = new Mock<IConnection>();
            _channelMock =  new Mock<IModel>();
            _connectionStorageMock = new Mock<IConnectionStorage>();
            _routeSettingsMock = new Mock<IRouteSettings>();
            _nameFactoryMock = new Mock<INameFactory>();
            _firstExchangeMock = new Mock<IExchange>();
            _secondExchangeMock = new Mock<IExchange>();
            _firstExchangeMock.Setup(x => x.Exchange).Returns(ExchangeLookup.Task);
            _secondExchangeMock.Setup(x => x.Exchange).Returns(ExchangeLookup.Permissions);

            _nameFactoryMock.Setup(x => x.GetExchange(It.IsAny<ExchangeLookup>()))
                .Returns((ExchangeLookup exchange) => exchange.ToString());

            _nameFactoryMock.Setup(x=>x.GetQueue(It.IsAny<string>(), It.IsAny<EventLookup>()))
                .Returns((string applicationName, EventLookup e) => $"{applicationName}_{e.ToString()}");

            _nameFactoryMock.Setup(x => x.GetRoutingKey(It.IsAny<EventLookup>()))
                .Returns((EventLookup e) => e.ToString());

            _connectionStorageMock.Setup(x => x.Get()).Returns(_connectionMock.Object);
            _connectionMock.Setup(x => x.CreateModel()).Returns(_channelMock.Object);
            _routeSettingsMock.Setup(x => x.ApplicationName).Returns(ApplicationName);
            _routeSettingsMock.Setup(x => x.Exchanges)
                .Returns(new[] {_firstExchangeMock.Object, _secondExchangeMock.Object});
            _routeInitializer = new RouteInitializer(_connectionStorageMock.Object, _routeSettingsMock.Object, _nameFactoryMock.Object);
        }

        [Fact]
        public void ShouldReturnIfNoAnyRoutes()
        {
            _routeSettingsMock.Setup(x => x.Routes).Returns(new IRoute[0]);

            _routeInitializer.Init();

            _connectionStorageMock.Verify(x => x.Get(), Times.Never);
            _connectionMock.Verify(x => x.CreateModel(), Times.Never);
            _channelMock.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public void ShouldCreateExchangeOnlyIfThereIsRelatedRoute()
        {
            var routeMock = new Mock<IRoute>();
            _routeSettingsMock.Setup(x => x.Routes).Returns(new []{ routeMock.Object});
            _routeInitializer.Init();
            _channelMock.Verify(x =>
                x.ExchangeDeclare(ExchangeLookup.Task.ToString(), ExchangeType.Direct, false, false, null), Times.Once);
            _channelMock.Verify(x =>
                x.ExchangeDeclare(ExchangeLookup.Permissions.ToString(), ExchangeType.Direct, false, false, null), Times.Never);
        }

        [Fact]
        public void ShouldDeclareQueueAndBindItToExchange()
        {
            var e = EventLookup.TaskStatusUpdated;
            var exchange = ExchangeLookup.Task;
            var routeMock = new Mock<IRoute>();
            var queue = $"{ApplicationName}_{e.ToString()}";
            routeMock.Setup(x => x.Event).Returns(e);
            _routeSettingsMock.Setup(x => x.Routes).Returns(new[] { routeMock.Object });

            _routeInitializer.Init();

            _channelMock.Verify(x => x.QueueDeclare(queue,
                false,
                false,
                false,
                null), Times.Once);

            _channelMock.Verify(x=>x.QueueBind(queue, exchange.ToString(), e.ToString(), null));
        }
    }
}