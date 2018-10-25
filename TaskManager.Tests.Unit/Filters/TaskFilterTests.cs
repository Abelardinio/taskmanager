using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using TaskManager.Core;
using TaskManager.DbConnection.Entities;
using TaskManager.WebApi.Model;

namespace TaskManager.Tests.Unit.Filters
{
    [TestFixture]
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
        private DateTime _firstTaskAdded;
        private DateTime _secondTaskAdded;
        private DateTime _thirdTaskAdded;
        private DateTime _firstTaskTimeToComplete;
        private DateTime _secondTaskTimeToComplete;
        private DateTime _thirdTaskTimeToComplete;
        private IQueryable<ITask> _tasks;

        [SetUp]
        public void SetUp()
        {
            _firstTaskAdded = _today.AddDays(1);
            _secondTaskAdded = _today.AddDays(5);
            _thirdTaskAdded = _today.AddDays(3);
            _firstTaskTimeToComplete = _today.AddDays(8);
            _secondTaskTimeToComplete = _today.AddDays(10);
            _thirdTaskTimeToComplete = _today.AddDays(12);
            _tasks = new List<ITask>
            {
                new TaskEntity
                {
                    Id = FirstTaskId,
                    Name = FirstTaskName,
                    Priority = FirstTaskPriority,
                    Added = _firstTaskAdded,
                    TimeToComplete = _firstTaskTimeToComplete
                },
                new TaskEntity
                {
                    Id = SecondTaskId,
                    Name = SecondTaskName,
                    Priority = SecondTaskPriority,
                    Added = _secondTaskAdded,
                    TimeToComplete = _secondTaskTimeToComplete
                },
                new TaskEntity
                {
                    Id = ThirdTaskId,
                    Name = ThirdTaskName,
                    Priority = ThirdTaskPriority,
                    Added = _thirdTaskAdded,
                    TimeToComplete = _thirdTaskTimeToComplete
                }
            }.AsQueryable();
        }

        [TestCase(TaskSortingColumn.Name)]
        [TestCase(TaskSortingColumn.Priority)]
        [TestCase(TaskSortingColumn.Added)]
        [TestCase(TaskSortingColumn.TimeToComplete)]
        public void SortTest(TaskSortingColumn sortingColumn)
        {
            var filter = new TaskFilter { SortingColumn = sortingColumn, SortingOrder = SortingOrder.Asc };
            var result = filter.Sort(_tasks).ToList();
            result.Should().BeInAscendingOrder(SortKeySelectorDictionary[sortingColumn]);
            filter = new TaskFilter { SortingColumn = sortingColumn, SortingOrder = SortingOrder.Desc };
            result = filter.Sort(_tasks).ToList();
            result.Should().BeInDescendingOrder(SortKeySelectorDictionary[sortingColumn]);
        }

        [TestCase(null, new[] { FirstTaskId, SecondTaskId, ThirdTaskId })]
        [TestCase("Fir", new [] { FirstTaskId })]
        [TestCase("ir", new[] { FirstTaskId, ThirdTaskId })]
        [TestCase("dT", new[] { SecondTaskId, ThirdTaskId })]
        [TestCase("randomString", new int[] { })]
        public void FilterByNameTest(string filterName, int[] taskIds )
        {
            var filter = new TaskFilter { Name = filterName };
            var result = filter.Filter(_tasks).ToList();
            AssertOnlyConatainsItems(result, taskIds);
        }

        [TestCase(6, 9, new int[] { })]
        [TestCase(null, null, new [] { FirstTaskId, SecondTaskId, ThirdTaskId})]
        [TestCase(2, null, new[] { SecondTaskId, ThirdTaskId })]
        [TestCase(null, 4, new[] { FirstTaskId, ThirdTaskId })]
        [TestCase(2, 6, new[] { SecondTaskId, ThirdTaskId })]
        public void FilterByAddedTest(int? numberOfdaysFrom, int? numberOfdaysTo, int[] taskIds)
        {
            var filter = new TaskFilter();

            if (numberOfdaysFrom != null)
            {
                filter.AddedFrom = _today.AddDays(numberOfdaysFrom.Value);
            }

            if (numberOfdaysTo != null)
            {
                filter.AddedTo = _today.AddDays(numberOfdaysTo.Value);
            }

            var result = filter.Filter(_tasks).ToList();
            AssertOnlyConatainsItems(result, taskIds);
        }

        [TestCase(6, 9, new int[] { })]
        [TestCase(null, null, new[] { FirstTaskId, SecondTaskId, ThirdTaskId })]
        [TestCase(2, null, new[] { FirstTaskId, ThirdTaskId })]
        [TestCase(null, 4, new[] { FirstTaskId, SecondTaskId })]
        [TestCase(2, 6, new[] { FirstTaskId, ThirdTaskId })]
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