using TaskManager.Core;

namespace TaskManager.DbConnection
{
    public class ConnectionContext : IConnectionContext, IContextFactory
    {
        private Context _context;
        private readonly object _sync = new object();

        public IConnectionScope Scope()
        {
            if (_context == null)
            {
                lock (_sync)
                {
                    if (_context == null)
                    {
                        _context = new Context();
                    }
                }
            }

           return _context;
        }

        public Context Get()
        {
            return _context;
        }
    }
}