using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.Core.ConnectionContext;
using TaskManager.DbConnection.Entities;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace TaskManager.DbConnection
{
    public class Context : DbContext, IDatabaseScope
    {
        private const string ConnectionString = "Server=SUZDORF10P01\\SQLSERVER2008;Database=TaskManager;User ID=admin;Password=Hello@123;";
        private readonly bool _isInTransactionScope;
        private readonly IDbContextTransaction _transaction;

        public Context(bool isInTransactionScope) : base()
        {
            _isInTransactionScope = isInTransactionScope;

            if (isInTransactionScope)
            {
                _transaction = Database.BeginTransaction();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        public virtual DbSet<TaskEntity> Tasks { get; set; }

        public bool IsDisposed { get; private set; }

        public void Commit()
        {
            if (_isInTransactionScope)
            {
                _transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (_isInTransactionScope)
            {
                _transaction.Rollback();
            }
        }

        public override void Dispose()
        {
            if (_isInTransactionScope)
            {
                _transaction.Dispose();
            }

            base.Dispose();
            IsDisposed = true;
        }
    }
}