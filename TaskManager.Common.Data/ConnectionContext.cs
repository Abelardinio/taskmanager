using TaskManager.Core.ConnectionContext;

namespace TaskManager.Common.Data
{
    public class ConnectionContext : IConnectionContext
    {
        private readonly IConnectionScopeFactory _factory;
        private readonly IEventScopeFactory _eventScopeFactory;
        private IDatabaseScope _scope;
        private IEventConnectionScope _eventScope;

        public ConnectionContext(IConnectionScopeFactory factory, IEventScopeFactory eventScopeFactory)
        {
            _factory = factory;
            _eventScopeFactory = eventScopeFactory;
        }

        public IConnectionScope Scope()
        {
            if (_scope == null || _scope.IsDisposed)
            {
                _scope =_factory.Create(false);
            }

            return _scope;
        }

        public ITransactionScope TransactionScope()
        {
            if (_scope == null || _scope.IsDisposed )
            {
                _scope = _factory.Create(true);
            }

            return _scope;
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