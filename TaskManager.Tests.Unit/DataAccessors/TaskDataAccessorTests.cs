using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TaskManager.Common.Resources;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Exceptions;
using TaskManager.DbConnection;
using TaskManager.DbConnection.DataAccessors;
using TaskManager.DbConnection.Entities;
using TaskManager.Tests.Unit.Stubs;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Tests.Unit.DataAccessors
{
    public class TaskDataAccessorTests
    {
        private const int TaskId = 5;
        private const int ActiveTaskId = 6;
        private const int CompletedTaskId = 7;
        private const int RemovedTaskId = 8;
        private const int NotFoundTaskId = 9;
        private TaskEntity _task;
        private TaskEntity _activeTask;
        private TaskEntity _completedTask;
        private TaskEntity _removedTask;
        private Mock<Context> _contextMock;
        private Mock<IContextStorage> _contextStorageMock;
        private ITaskDataAccessor _taskDataAccessor;


        private List<TaskEntity> _tasks;

        [SetUp]
        public void SetUp()
        {
            _task = new TaskEntity
            {
                Id = TaskId,
                Status = TaskStatus.None
            };

            _activeTask = new TaskEntity
            {
                Id = ActiveTaskId,
                Status = TaskStatus.Active
            };

            _completedTask = new TaskEntity
            {
                Id = CompletedTaskId,
                Status = TaskStatus.Completed
            };

            _removedTask = new TaskEntity
            {
                Id = RemovedTaskId,
                Status = TaskStatus.Removed
            };

            _contextMock =  new Mock<Context>(null);
            _contextStorageMock = new Mock<IContextStorage>();

            _contextMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);
            _contextStorageMock.Setup(x => x.Get()).Returns(_contextMock.Object);

            _tasks = new List<TaskEntity> { _task, _activeTask, _completedTask, _removedTask };

            _contextMock.SetupGet(x => x.Tasks).Returns(new StubSet<TaskEntity>(_tasks));

            _taskDataAccessor = new TaskDataAccessor(_contextStorageMock.Object);
        }

        [TestCase(TaskId)]
        [TestCase(CompletedTaskId)]
        [TestCase(RemovedTaskId)]
        public void CompleteNotActiveTaskFailTest(int taskId)
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(taskId, TaskStatus.Completed).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_CompleteUnactive);
        }

        [TestCase(TaskId)]
        [TestCase(ActiveTaskId)]
        [TestCase(RemovedTaskId)]
        public void RemoveNotCompletedTaskFailTest(int taskId)
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(taskId, TaskStatus.Removed).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_RemoveUncompleted);
        }

        [TestCase(TaskStatus.Active)]
        [TestCase(TaskStatus.None)]
        public void InvalidStatusUpdateFailTest(TaskStatus status)
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(TaskId, status).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_InvalidStatusParameterValue);
        }

        [Test]
        public void CompleteTaskSuccessTest()
        {
            _taskDataAccessor.UpdateStatusAsync(ActiveTaskId, TaskStatus.Completed).Wait();
            _contextMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public void RemoveTaskSuccessTest()
        {
            _taskDataAccessor.UpdateStatusAsync(CompletedTaskId, TaskStatus.Removed).Wait();
            _contextMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public void TaskNotFoundFailTest()
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(NotFoundTaskId, TaskStatus.Removed).Wait(); };
            action.Should().Throw<NotFoundException>().WithMessage(String.Format(ErrorMessages.Tasks_NotFound, NotFoundTaskId));
        }
    }
}