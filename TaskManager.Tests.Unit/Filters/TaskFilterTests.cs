using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;
using TaskManager.Core;
using TaskManager.DbConnection.Entities;
using TaskManager.WebApi.Model;

namespace TaskManager.Tests.Unit.Filters
{
    public class TaskFilterTests
    {
        private const int FirstTaskId = 1;
        private const int SecondTaskId = 2;
        private const int ThirdTaskId = 3;
        private const string FirstTaskName = "FirstTaskName";
        private const string SecondTaskName = "SecondTaskName";
        private const string ThirdTaskName = "ThirdTaskName";
        private const int FirstTaskPriority = 3;
        private const int SecondTaskPriority = 1;
        private const int ThirdTaskPriority = 5;
        private readonly DateTime _today = DateTime.Today;
        private readonly IQueryable<ITask> _tasks;
        
        public TaskFilterTests()
        {
            var firstTaskAdded = _today.AddDays(1);
            var secondTaskAdded = _today.AddDays(5);
            var thirdTaskAdded = _today.AddDays(3);
            var firstTaskTimeToComplete = _today.AddDays(8);
            var secondTaskTimeToComplete = _today.AddDays(10);
            var thirdTaskTimeToComplete = _today.AddDays(12);
            _tasks = new List<ITask>
            {
                new TaskEntity
                {
                    Id = FirstTaskId,
                    Name = FirstTaskName,
                    Priority = FirstTaskPriority,
                    Added = firstTaskAdded,
                    TimeToComplete = firstTaskTimeToComplete
                },
                new TaskEntity
                {
                    Id = SecondTaskId,
                    Name = SecondTaskName,
                    Priority = SecondTaskPriority,
                    Added = secondTaskAdded,
                    TimeToComplete = secondTaskTimeToComplete
                },
                new TaskEntity
                {
                    Id = ThirdTaskId,
                    Name = ThirdTaskName,
                    Priority = ThirdTaskPriority,
                    Added = thirdTaskAdded,
                    TimeToComplete = thirdTaskTimeToComplete
                }
            }.AsQueryable();
        }

        [Theory]
        [InlineData(TaskSortingColumn.Name)]
        [InlineData(TaskSortingColumn.Priority)]
        [InlineData(TaskSortingColumn.Added)]
        [InlineData(TaskSortingColumn.TimeToComplete)]
        public void SortTest(TaskSortingColumn sortingColumn)
        {
            var filter = new TaskFilter { SortingColumn = sortingColumn, SortingOrder = SortingOrder.Asc };
            var result = filter.Sort(_tasks).ToList();
            result.Should().BeInAscendingOrder(SortKeySelectorDictionary[sortingColumn]);
            filter = new TaskFilter { SortingColumn = sortingColumn, SortingOrder = SortingOrder.Desc };
            result = filter.Sort(_tasks).ToList();
            result.Should().BeInDescendingOrder(SortKeySelectorDictionary[sortingColumn]);
        }

        [Theory]
        [InlineData(null, new[] { FirstTaskId, SecondTaskId, ThirdTaskId })]
        [InlineData("Fir", new[] { FirstTaskId })]
        [InlineData("ir", new[] { FirstTaskId, ThirdTaskId })]
        [InlineData("dT", new[] { SecondTaskId, ThirdTaskId })]
        [InlineData("randomString", new int[] { })]
        public void FilterByNameTest(string filterName, int[] taskIds )
        {
            var filter = new TaskFilter { Name = filterName };
            var result = filter.Filter(_tasks).ToList();
            AssertOnlyConatainsItems(result, taskIds);
        }

        [Theory]
        [InlineData(6, 9, new int[] { })]
        [InlineData(null, null, new[] { FirstTaskId, SecondTaskId, ThirdTaskId })]
        [InlineData(2, null, new[] { SecondTaskId, ThirdTaskId })]
        [InlineData(null, 4, new[] { FirstTaskId, ThirdTaskId })]
        [InlineData(2, 6, new[] { SecondTaskId, ThirdTaskId })]
        public void FilterByAddedTest(int? numberOfDaysFrom, int? numberOfDaysTo, int[] taskIds)
        {
            var filter = new TaskFilter();

            if (numberOfDaysFrom != null)
            {
                filter.AddedFrom = _today.AddDays(numberOfDaysFrom.Value);
            }

            if (numberOfDaysTo != null)
            {
                filter.AddedTo = _today.AddDays(numberOfDaysTo.Value);
            }

            var result = filter.Filter(_tasks).ToList();
            AssertOnlyConatainsItems(result, taskIds);
        }

        [Theory]
        [InlineData(6, 9, new int[] { })]
        [InlineData(null, null, new[] { FirstTaskId, SecondTaskId, ThirdTaskId })]
        [InlineData(2, null, new[] { FirstTaskId, ThirdTaskId })]
        [InlineData(null, 4, new[] { FirstTaskId, SecondTaskId })]
        [InlineData(2, 6, new[] { FirstTaskId, ThirdTaskId })]
        public void FilterByPriorityTest(int? priorityFrom, int? priorityTo, int[] taskIds)
        {
            var filter = new TaskFilter
            {
                PriorityFrom = priorityFrom,
                PriorityTo = priorityTo
            };

            var result = filter.Filter(_tasks).ToList();
            AssertOnlyConatainsItems(result, taskIds);
        }

        private void AssertOnlyConatainsItems(List<ITask> tasks, int[] taskIds)
        {
            tasks.Should().HaveCount(taskIds.Length);

            taskIds.ToList().ForEach(x =>
            {
                tasks.Should().Contain(y => y.Id == x);
            });
        }

        private static readonly IDictionary<TaskSortingColumn, Expression<Func<ITask, object>>> SortKeySelectorDictionary =
            new Dictionary<TaskSortingColumn, Expression<Func<ITask, object>>>
            {
                {TaskSortingColumn.Name, x => x.Name},
                {TaskSortingColumn.Priority, x => x.Priority},
                {TaskSortingColumn.Added, x => x.Added},
                {TaskSortingColumn.TimeToComplete, x => x.TimeToComplete}
            };
        }
}