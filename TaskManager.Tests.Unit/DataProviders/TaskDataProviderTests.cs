using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TaskManager.Common.Resources;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Exceptions;
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
        private const int NotFoundTaskId = 9;
        private Mock<ITaskDataAccessor> _taskDataAccessorMock;
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

            _tasks = new List<ITask> { _task, _activeTask, _completedTask, _removedTask };

            _taskDataAccessorMock.Setup(x => x.Get()).Returns(new StubSet<ITask>(_tasks));
            _taskDataAccessorMock.Setup(x => x.UpdateStatus(It.IsAny<int>(), It.IsAny<TaskStatus>()))
                .Returns(() =>Task.Run(() => { }));
            _taskDataProvider = new TaskDataProvider(_taskDataAccessorMock.Object);
        }

        [TestCase(TaskId)]
        [TestCase(CompletedTaskId)]
        [TestCase(RemovedTaskId)]
        public void CompleteUnactiveTaskFailTest(int taskId)
        {
            Action action = () => { _taskDataProvider.UpdateStatusAsync(taskId, TaskStatus.Completed).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_CompleteUnactive);
        }

        [TestCase(TaskId)]
        [TestCase(ActiveTaskId)]
        [TestCase(RemovedTaskId)]
        public void RemoveUncomplitedTaskFailTest(int taskId)
        {
            Action action = () => { _taskDataProvider.UpdateStatusAsync(taskId, TaskStatus.Removed).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_RemoveUncompleted);
        }

        [TestCase(TaskStatus.Active)]
        [TestCase(TaskStatus.None)]
        public void InvalidStatusUpdateFailTest(TaskStatus status)
        {
            Action action = () => { _taskDataProvider.UpdateStatusAsync(TaskId, status).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_InvalidStatusParameterValue);
        }

        [Test]
        public void CompleteTaskSuccessTest()
        {
            _taskDataProvider.UpdateStatusAsync(ActiveTaskId, TaskStatus.Completed).Wait();
            _taskDataAccessorMock.Verify(x => x.UpdateStatus(ActiveTaskId, TaskStatus.Completed), Times.Once());
        }

        [Test]
        public void RemoveTaskSuccessTest()
        {
            _taskDataProvider.UpdateStatusAsync(CompletedTaskId, TaskStatus.Removed).Wait();
            _taskDataAccessorMock.Verify(x => x.UpdateStatus(CompletedTaskId, TaskStatus.Removed), Times.Once());
        }

        [Test]
        public void TaskNotFoundFailTest()
        {
            Action action = () => { _taskDataProvider.UpdateStatusAsync(NotFoundTaskId, TaskStatus.Removed).Wait(); };
            action.Should().Throw<NotFoundException>().WithMessage(String.Format(ErrorMessages.Tasks_NotFound, NotFoundTaskId));
        }

        [Test]
        public void GetUnremovedTasksTest()
        {
            var tasks = _taskDataProvider.GetUnremoved().ToList();
            tasks.Count.Should().Be(2);
            tasks.Should().Contain(x => x.Id == ActiveTaskId);
            tasks.Should().Contain(x => x.Id == CompletedTaskId);
        }
    }
}