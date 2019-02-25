using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.Core.ConnectionContext;

namespace TaskManager.Common.DbConnection
{
    public abstract class ContextBase : DbContext, IDatabaseScope
    {
        private readonly string _connectionString;
        private readonly bool _isInTransactionScope;
        private readonly IDbContextTransaction _transaction;

        public bool IsDisposed { get; private set; }

        protected ContextBase(string connectionString, bool isInTransactionScope)
        {
            _isInTransactionScope = isInTransactionScope;
            _connectionString = connectionString;

            if (isInTransactionScope)
            {
                _transaction = Database.BeginTransaction();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, o => o.UseRowNumberForPaging());
            }
        }

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