using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class TaskFilter : IFilter<ITask>
    {
        public SortingOrder SortingOrder { get; set; }
        public int SortingColumn { get; set; }
        public string Name { get; set; }
        public DateTime? AddedFrom { get; set; }
        public DateTime? AddedTo { get; set; }
        public int? PriorityFrom { get; set; }
        public int? PriorityTo { get; set; }

        public IQueryable<ITask> Sort(IQueryable<ITask> input)
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

        private static readonly IDictionary<int, Func<IQueryable<ITask>, IQueryable<ITask>>> SortingDictionaryAsc =
            new Dictionary<int, Func<IQueryable<ITask>, IQueryable<ITask>>>
            {
                {0, x => x.OrderBy(y => y.Name)},
                {1, x => x.OrderBy(y => y.Priority)},
                {2, x => x.OrderBy(y => y.Added)},
                {3, x => x.OrderBy(y => y.TimeToComplete)}
            };

        private static readonly IDictionary<int, Func<IQueryable<ITask>, IQueryable<ITask>>> SortingDictionaryDesc =
            new Dictionary<int, Func<IQueryable<ITask>, IQueryable<ITask>>>
            {
                {0, x => x.OrderByDescending(y => y.Name)},
                {1, x => x.OrderByDescending(y => y.Priority)},
                {2, x => x.OrderByDescending(y => y.Added)},
                {3, x => x.OrderByDescending(y => y.TimeToComplete)}
            };

        public IQueryable<ITask> Filter(IQueryable<ITask> input)
        {
            return input.Where(x => (string.IsNullOrEmpty(Name) || x.Name.Contains(Name)) &&
                                    (!AddedFrom.HasValue || x.Added >= AddedFrom) &&
                                    (!AddedTo.HasValue || x.Added <= AddedTo) &&
                                    (!PriorityFrom.HasValue || x.Priority >= PriorityFrom) &&
                                    (!PriorityTo.HasValue || x.Priority <= PriorityTo));
        }
    }
}
