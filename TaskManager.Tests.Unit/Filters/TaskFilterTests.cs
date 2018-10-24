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
        private const int FirstTaskPriority = 2;
        private const int SecondTaskPriority = 1;
        private const int ThirdTaskPriority = 3;
        private readonly DateTime _firstTaskAdded = DateTime.Today.AddDays(1);
        private readonly DateTime _secondTaskAdded = DateTime.Today.AddDays(3);
        private readonly DateTime _thirdTaskAdded = DateTime.Today.AddDays(2);
        private readonly DateTime _firstTaskTimeToComplete = DateTime.Today.AddDays(8);
        private readonly DateTime _secondTaskTimeToComplete = DateTime.Today.AddDays(9);
        private readonly DateTime _thirdTaskTimeToComplete = DateTime.Today.AddDays(10);
        private IQueryable<ITask> _tasks;

        [SetUp]
        public void SetUp()
        {
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