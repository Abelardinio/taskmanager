using Microsoft.EntityFrameworkCore;
using TaskManager.AuthService.DbConnection.Entities;
using TaskManager.Common.DbConnection;

namespace TaskManager.AuthService.DbConnection
{
    public class Context : ContextBase
    {
        public Context(string connectionString, bool isInTransactionScope) : base(connectionString, isInTransactionScope)
        {
        }

        public virtual DbSet<UserEntity> Tasks { get; set; }
    }
}
