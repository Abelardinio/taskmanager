using Microsoft.EntityFrameworkCore;
using TaskManager.Common.DbConnection;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection
{
    public class Context : ContextBase
    {
        public Context(string connectionString, bool isInTransactionScope) : base (connectionString, isInTransactionScope)
        {
        }

        public virtual DbSet<TaskEntity> Tasks { get; set; }
    }
}