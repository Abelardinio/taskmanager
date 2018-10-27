using TaskManager.Core.ConnectionContext;

namespace TaskManager.Data
{
    public class ConnectionContext : IConnectionContext
    {
        private readonly IConnectionScopeFactory _factory;
        private IDatabaseScope _scope;

        public ConnectionContext(IConnectionScopeFactory factory)
        {
            _factory = factory;
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
    }
}