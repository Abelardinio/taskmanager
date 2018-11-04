using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskManager.Data.DataProviders;
using TaskManager.DbConnection.Entities;
using TaskManager.Tests.Unit.Stubs;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Tests.Unit.DataProviders
{
    [TestFixture]
    public class TaskDataProviderTests
    {
        private const int TaskId = 5;
        private const int ActiveTaskId = 6;
        private const int CompletedTaskId = 7;
        private const int RemovedTaskId = 8;
        private Mock<ITaskDataAccessor> _taskDataAccessorMock;
        private Mock<ITaskEventAccessor> _taskEventAccessorMock;
        private Mock<IConnectionContext> _connectionContextMock;
        private ITaskDataProvider _taskDataProvider;

        private readonly ITask _task = new TaskEntity
        {
            Id = TaskId,
            Status = TaskStatus.None
        };

        private readonly ITask _activeTask = new TaskEntity
        {
            Id = ActiveTaskId,
            Status = TaskStatus.Active
        };

        private readonly ITask _completedTask = new TaskEntity
        {
            Id = CompletedTaskId,
            Status = TaskStatus.Completed
        };

        private readonly ITask _removedTask = new TaskEntity
        {
            Id = RemovedTaskId,
            Status = TaskStatus.Removed
        };

        private List<ITask> _tasks;


        [SetUp]
        public void SetUp()
        {
            _taskDataAccessorMock = new Mock<ITaskDataAccessor>();
            _taskEventAccessorMock = new Mock<ITaskEventAccessor>();
            _connectionContextMock = new Mock<IConnectionContext>();

            _tasks = new List<ITask> { _task, _activeTask, _completedTask, _removedTask };

            _taskDataAccessorMock.Setup(x => x.Get()).Returns(new StubSet<ITask>(_tasks));

            _taskDataProvider = new TaskDataProvider(
                _taskDataAccessorMock.Object,
                _taskEventAccessorMock.Object,
                _connectionContextMock.Object);
        }

        [Test]
        public void GetLiveTasksTest()
        {
            var tasks = _taskDataProvider.GetLiveTasks().ToList();
            tasks.Count.Should().Be(2);
            tasks.Should().Contain(x => x.Id == ActiveTaskId);
            tasks.Should().Contain(x => x.Id == CompletedTaskId);
        }
    }
}