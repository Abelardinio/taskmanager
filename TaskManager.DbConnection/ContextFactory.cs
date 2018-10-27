using TaskManager.Core.ConnectionContext;

namespace TaskManager.DbConnection
{
    public class ContextFactory : IContextStorage, IConnectionScopeFactory
    {
        private Context _context;

        public Context Get()
        {
            return _context;
        }

        public IDatabaseScope Create(bool isInTransactionScope)
        {
            _context = new Context(isInTransactionScope);

            return _context;
        }
    }
}