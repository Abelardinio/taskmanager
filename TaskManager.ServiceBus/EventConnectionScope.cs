using RabbitMQ.Client;
using TaskManager.Core.ConnectionContext;

namespace TaskManager.ServiceBus
{
    public class EventConnectionScope : IEventConnectionScope
    {
        private readonly IModel _channel;
        public EventConnectionScope(IModel channel)
        {
            _channel = channel;
        }
        public void Dispose()
        {
            _channel.Dispose();
            IsDisposed = true;
        }

        public bool IsDisposed { get; private set; }
    }
}