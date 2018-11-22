using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Core;

namespace TaskManager.Common.AspNetCore
{
    public abstract class BaseFilter<TSortingColumn, T> : IFilter<T> where TSortingColumn : struct, IConvertible
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public SortingOrder SortingOrder { get; set; }
        public TSortingColumn SortingColumn { get; set; }

        public IQueryable<T> Sort(IQueryable<T> input)
        {
            switch (SortingOrder)
            {
                case SortingOrder.Asc:
                    return SortingDictionaryAsc[SortingColumn](input);
                case SortingOrder.Desc:
                    return SortingDictionaryDesc[SortingColumn](input);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract IQueryable<T> Filter(IQueryable<T> input);

        protected abstract IDictionary<TSortingColumn, Func<IQueryable<T>, IQueryable<T>>> SortingDictionaryAsc { get; }
        protected abstract IDictionary<TSortingColumn, Func<IQueryable<T>, IQueryable<T>>> SortingDictionaryDesc { get; }
    }
}