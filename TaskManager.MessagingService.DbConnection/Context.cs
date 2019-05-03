using Microsoft.EntityFrameworkCore;
using TaskManager.Common.DbConnection;
using TaskManager.MessagingService.DbConnection.Entities;

namespace TaskManager.MessagingService.DbConnection
{
    public class Context : ContextBase
    {
        public Context(string connectionString, bool isInTransactionScope) : base(connectionString, isInTransactionScope)
        {
        }

        public virtual DbSet<UserPermissionEntity> Permissions { get; set; }
    }
}
