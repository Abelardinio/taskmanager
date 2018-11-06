using TaskManager.Core.ConnectionContext;

namespace TaskManager.MessagingService
{
    public class ConnectionContext : IEventConnectionContext
    {
        private readonly IEventScopeFactory _eventScopeFactory;
        private IEventConnectionScope _eventScope;

        public ConnectionContext(IEventScopeFactory eventScopeFactory)
        {
            _eventScopeFactory = eventScopeFactory;
        }

        public IEventScope EventScope()
        {
            if (_eventScope == null || _eventScope.IsDisposed)
            {
                _eventScope = _eventScopeFactory.Create();
            }

            return _eventScope;
        }
    }
}