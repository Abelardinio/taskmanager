using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Common.Api;
using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class TaskFilter : BaseFilter<TaskSortingColumn, ITask>
    {
        public string Name { get; set; }
        public DateTime? AddedFrom { get; set; }
        public DateTime? AddedTo { get; set; }
        public int? PriorityFrom { get; set; }
        public int? PriorityTo { get; set; }

        public override IQueryable<ITask> Filter(IQueryable<ITask> input)
        {
            return input.Where(x => (string.IsNullOrEmpty(Name) || x.Name.Contains(Name)) &&
                                    (!AddedFrom.HasValue || x.Added >= AddedFrom) &&
                                    (!AddedTo.HasValue || x.Added <= AddedTo) &&
                                    (!PriorityFrom.HasValue || x.Priority >= PriorityFrom) &&
                                    (!PriorityTo.HasValue || x.Priority <= PriorityTo));
        }

        protected override IDictionary<TaskSortingColumn, Func<IQueryable<ITask>, IQueryable<ITask>>> SortingDictionaryAsc => SortDictionaryAsc;

        protected override IDictionary<TaskSortingColumn, Func<IQueryable<ITask>, IQueryable<ITask>>> SortingDictionaryDesc => SortDictionaryDesc;

        private static readonly IDictionary<TaskSortingColumn, Func<IQueryable<ITask>, IQueryable<ITask>>> SortDictionaryAsc =
            new Dictionary<TaskSortingColumn, Func<IQueryable<ITask>, IQueryable<ITask>>>
            {
                {TaskSortingColumn.Name, x => x.OrderBy(y => y.Name)},
                {TaskSortingColumn.Priority, x => x.OrderBy(y => y.Priority)},
                {TaskSortingColumn.Added, x => x.OrderBy(y => y.Added)},
                {TaskSortingColumn.TimeToComplete, x => x.OrderBy(y => y.TimeToComplete)}
            };

        private static readonly IDictionary<TaskSortingColumn, Func<IQueryable<ITask>, IQueryable<ITask>>> SortDictionaryDesc =
            new Dictionary<TaskSortingColumn, Func<IQueryable<ITask>, IQueryable<ITask>>>
            {
                {TaskSortingColumn.Name, x => x.OrderByDescending(y => y.Name)},
                {TaskSortingColumn.Priority, x => x.OrderByDescending(y => y.Priority)},
                {TaskSortingColumn.Added, x => x.OrderByDescending(y => y.Added)},
                {TaskSortingColumn.TimeToComplete, x => x.OrderByDescending(y => y.TimeToComplete)}
            };
    }
}
