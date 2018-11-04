using RabbitMQ.Client;
using TaskManager.Core.ConnectionContext;

namespace TaskManager.ServiceBus
{
    public class ChannelFactory : IEventScopeFactory, IChannelStorage
    {
        private readonly IConnectionStorage _storage;
        private IModel _channel;

        public ChannelFactory(IConnectionStorage storage)
        {
            _storage = storage;
        }

        public IEventConnectionScope Create()
        {
            _channel = _storage.Get().CreateModel();
            return new EventConnectionScope(_channel);
        }

        public IModel Get()
        {
            return _channel;
        }
    }
}