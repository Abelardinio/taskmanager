using System.Data.Entity;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection
{
    internal class Context : DbContext
    {
        internal Context() : base("DbConnection")
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}