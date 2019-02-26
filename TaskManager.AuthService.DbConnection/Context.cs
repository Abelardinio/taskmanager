using System;
using TaskManager.Common.DbConnection;

namespace TaskManager.AuthService.DbConnection
{
    public class Context : ContextBase
    {
        public Context(string connectionString, bool isInTransactionScope) : base(connectionString, isInTransactionScope)
        {
        }
    }
}
