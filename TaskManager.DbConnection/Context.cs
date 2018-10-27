using System.Data.Entity;
using TaskManager.Core.ConnectionContext;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection
{
    public class Context : DbContext, IDatabaseScope
    {
        private readonly bool _isInTransactionScope;
        private readonly DbContextTransaction _transaction;

        public Context(bool isInTransactionScope) : base("DbConnection")
        {
            _isInTransactionScope = isInTransactionScope;

            if (isInTransactionScope)
            {
                _transaction = Database.BeginTransaction();
            }
        }

        public DbSet<TaskEntity> Tasks { get; set; }

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

        protected override void Dispose(bool disposing)
        {
            if (_isInTransactionScope)
            {
                _transaction.Dispose();
            }

            base.Dispose(disposing);
            IsDisposed = true;
        }
    }
}