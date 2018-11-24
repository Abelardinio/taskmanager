using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using TaskManager.Common.Resources;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Exceptions;
using TaskManager.DbConnection;
using TaskManager.DbConnection.DataAccessors;
using TaskManager.DbConnection.Entities;
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
        private readonly Mock<Context> _contextMock;
        private readonly ITaskDataAccessor _taskDataAccessor;


        public TaskDataAccessorTests()
        {
            var task = new TaskEntity
            {
                Id = TaskId,
                Status = TaskStatus.None
            };

            var activeTask = new TaskEntity
            {
                Id = ActiveTaskId,
                Status = TaskStatus.Active
            };

            var completedTask = new TaskEntity
            {
                Id = CompletedTaskId,
                Status = TaskStatus.Completed
            };

            var removedTask = new TaskEntity
            {
                Id = RemovedTaskId,
                Status = TaskStatus.Removed
            };

            _contextMock = new Mock<Context>(null);
            var contextStorageMock = new Mock<IContextStorage>();

            _contextMock.Setup(x => x.SaveChangesAsync(default(CancellationToken))).ReturnsAsync(0);
            contextStorageMock.Setup(x => x.Get()).Returns(_contextMock.Object);

            var tasks = new List<TaskEntity> { task, activeTask, completedTask, removedTask }.AsQueryable();

            var mockSetTasks = new Mock<DbSet<TaskEntity>>();

            mockSetTasks.As<IAsyncEnumerable<TaskEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestDbAsyncEnumerator<TaskEntity>(tasks.GetEnumerator()));

            mockSetTasks.As<IQueryable<TaskEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TaskEntity>(tasks.Provider));

            mockSetTasks.As<IQueryable<TaskEntity>>().Setup(m => m.Expression).Returns(tasks.Expression);
            mockSetTasks.As<IQueryable<TaskEntity>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
            mockSetTasks.As<IQueryable<TaskEntity>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

            _contextMock.SetupGet(x => x.Tasks).Returns(mockSetTasks.Object);

            _taskDataAccessor = new TaskDataAccessor(contextStorageMock.Object);
        }

        [Theory]
        [InlineData(TaskId)]
        [InlineData(CompletedTaskId)]
        [InlineData(RemovedTaskId)]
        public void CompleteNotActiveTaskFailTest(int taskId)
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(taskId, TaskStatus.Completed).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_CompleteUnactive);
        }

        [Theory]
        [InlineData(TaskId)]
        [InlineData(ActiveTaskId)]
        [InlineData(RemovedTaskId)]
        public void RemoveNotCompletedTaskFailTest(int taskId)
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(taskId, TaskStatus.Removed).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_RemoveUncompleted);
        }

        [Theory]
        [InlineData(TaskStatus.Active)]
        [InlineData(TaskStatus.None)]
        public void InvalidStatusUpdateFailTest(TaskStatus status)
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(TaskId, status).Wait(); };
            action.Should().Throw<InvalidArgumentException>().WithMessage(ErrorMessages.Tasks_InvalidStatusParameterValue);
        }

        [Fact]
        public void CompleteTaskSuccessTest()
        {
            _taskDataAccessor.UpdateStatusAsync(ActiveTaskId, TaskStatus.Completed).Wait();
            _contextMock.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once());
        }

        [Fact]
        public void RemoveTaskSuccessTest()
        {
            _taskDataAccessor.UpdateStatusAsync(CompletedTaskId, TaskStatus.Removed).Wait();
            _contextMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
        }

        [Fact]
        public void TaskNotFoundFailTest()
        {
            Action action = () => { _taskDataAccessor.UpdateStatusAsync(NotFoundTaskId, TaskStatus.Removed).Wait(); };
            action.Should().Throw<NotFoundException>().WithMessage(String.Format(ErrorMessages.Tasks_NotFound, NotFoundTaskId));
        }
    }
}