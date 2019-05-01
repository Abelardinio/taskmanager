using System.Linq;
using RabbitMQ.Client;

namespace TaskManager.ServiceBus
{
    public class RouteInitializer : IRouteInitializer
    {
        private readonly IConnectionStorage _connectionStorage;
        private readonly IRouteSettings _routeSettings;
        private readonly INameFactory _nameFactory;

        public RouteInitializer(IConnectionStorage connectionStorage, IRouteSettings routeSettings, INameFactory nameFactory)
        {
            _connectionStorage = connectionStorage;
            _routeSettings = routeSettings;
            _nameFactory = nameFactory;
        }

        public void Init()
        {
            if (!_routeSettings.Routes.Any())
            {
                return;
            }

            var exchangesToDeclare =
                _routeSettings.Exchanges.Where(x => _routeSettings.Routes.Any(y => y.Exchange == x.Exchange));

            using (var channel = _connectionStorage.Get().CreateModel())
            {
                foreach (var exchange in exchangesToDeclare)
                {
                    channel.ExchangeDeclare(exchange.Exchange.ToString(), ExchangeType.Direct);
                }

                foreach (var route in _routeSettings.Routes)
                {
                    var routingKey = _nameFactory.GetRoutingKey(route.Event);
                    var queue = _nameFactory.GetQueue(_routeSettings.ApplicationName, route.Event);
                    var exchange = _nameFactory.GetExchange(route.Exchange);

                    channel.QueueDeclare(queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey);
                }
            }
        }
    }
}