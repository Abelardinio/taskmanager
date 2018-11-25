using TaskManager.Core;
using TaskManager.Core.ConnectionContext;

namespace TaskManager.DbConnection
{
    public class ContextFactory : IContextStorage, IConnectionScopeFactory
    {
        private readonly IDbConnectionSettings _settings;
        private Context _context;

        public ContextFactory(IDbConnectionSettings settings)
        {
            _settings = settings;
        }

        public Context Get()
        {
            return _context;
        }

        public IDatabaseScope Create(bool isInTransactionScope)
        {
            _context = new Context(_settings.ConnectionString, isInTransactionScope);

            return _context;
        }
    }
}