using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Tests.Unit.Stubs
{
    internal class StubSet<T> : EnumerableQuery<T>, IDbSet<T>, IDbAsyncQueryProvider
        where T : class
    {
        public StubSet(IEnumerable<T> collection) : base(collection)
        {
            Local = new ObservableCollection<T>(collection);
        }

        public ObservableCollection<T> Local { get; }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            Local.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            Local.Remove(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            return Add(entity);
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public void DeleteObject(T entity)
        {
            throw new NotImplementedException();
        }

        public void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        async Task<object> IDbAsyncQueryProvider.ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return ((IQueryProvider)this).Execute(expression);
        }

        async Task<TResult> IDbAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return ((IQueryProvider)this).Execute<TResult>(expression);
        }
    }
}
