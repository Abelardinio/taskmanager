using System.Data.Entity;
using TaskManager.Core;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection
{
    public class Context : DbContext, IConnectionScope
    {
        public Context() : base("DbConnection")
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}