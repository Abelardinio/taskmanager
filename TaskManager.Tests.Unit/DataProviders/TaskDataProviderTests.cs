using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TaskManager.Common.Resources;
using Xunit;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Exceptions;
using TaskManager.Data.DataProviders;
using TaskManager.DbConnection.Entities;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Tests.Unit.DataProviders
{
    public class TaskDataProviderTests
    {
        private const int UserId = 9;
        private const int TaskId = 5;
        private const int ActiveTaskId = 6;
        private const int CompletedTaskId = 7;
        private const int RemovedTaskId = 8;
        private readonly Mock<IProject> _projectMock =  new Mock<IProject>();
        private readonly Mock<ITaskDataAccessor> _taskDataAccessorMock;
        private readonly Mock<ITaskEventAccessor> _taskEventAccessorMock;
        private readonly Mock<IConnectionContext> _connectionContextMock;
        private readonly Mock<IProjectsDataAccessor> _projectsDataAccessorMock;
        private readonly Mock<IPermissionsDataAccessor> _permissionsDataAccessorMock;
        private readonly ITaskDataProvider _taskDataProvider;

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

        public TaskDataProviderTests()
        {
            _taskDataAccessorMock = new Mock<ITaskDataAccessor>();
            _taskEventAccessorMock = new Mock<ITaskEventAccessor>();
            _connectionContextMock = new Mock<IConnectionContext>();
            _projectsDataAccessorMock = new Mock<IProjectsDataAccessor>();
            _permissionsDataAccessorMock = new Mock<IPermissionsDataAccessor>();

            var tasks = new List<ITask> { _task, _activeTask, _completedTask, _removedTask };

            _taskDataAccessorMock.Setup(x => x.Get(1, null)).Returns(tasks.AsQueryable);
            _projectsDataAccessorMock.Setup(x => x.GetAsync(TaskId)).Returns(Task.FromResult(_projectMock.Object));
            _permissionsDataAccessorMock.Setup(x => x.HasPermissionForTask(UserId, TaskId))
                .Returns(Task.FromResult(true));

            _taskDataProvider = new TaskDataProvider(
                _taskDataAccessorMock.Object,
                _taskEventAccessorMock.Object,
                _connectionContextMock.Object,
                _permissionsDataAccessorMock.Object,
                _projectsDataAccessorMock.Object);
        }

        [Fact]
        public void GetLiveTasksTest()
        {
            var tasks = _taskDataProvider.GetLiveTasks(1,null).ToList();
            tasks.Count.Should().Be(2);
            tasks.Should().Contain(x => x.Id == ActiveTaskId);
            tasks.Should().Contain(x => x.Id == CompletedTaskId);
        }

        [Fact]
        public void AfterUpdateStatusSuccessShouldCallStatusUpdatedEventTest()
        {
            var updatedTask = new Mock<ITask>().Object;
            _taskDataAccessorMock.Setup(x => x.UpdateStatusAsync(TaskId, TaskStatus.Completed))
                .Returns(Task.FromResult(updatedTask));
            _taskDataProvider.UpdateStatusAsync(UserId, TaskId, TaskStatus.Completed);
            _taskDataAccessorMock.Verify(x => x.UpdateStatusAsync(TaskId, TaskStatus.Completed), Times.Once());
            _taskEventAccessorMock.Verify(x => x.StatusUpdated(updatedTask, _projectMock.Object), Times.Once());
            _connectionContextMock.Verify(x=>x.EventScope(), Times.Once);
        }

        [Fact]
        public void AfterUpdateStatusFailShouldNotCallStatusUpdatedEventTest()
        {
            _taskDataAccessorMock.Setup(x => x.UpdateStatusAsync(TaskId, TaskStatus.Completed)).Throws<Exception>();
            Action action = () =>
            {
                _taskDataProvider.UpdateStatusAsync(UserId, TaskId, TaskStatus.Completed).Wait();
            };

            action.Should().Throw<Exception>();
            _taskDataAccessorMock.Verify(x => x.UpdateStatusAsync(TaskId, TaskStatus.Completed), Times.Once());
            _taskEventAccessorMock.Verify(x => x.StatusUpdated(It.IsAny<ITask>(), _projectMock.Object), Times.Never);
            _connectionContextMock.Verify(x => x.EventScope(), Times.Never);
        }

        [Fact]
        public void ShouldThrowIfNoPermissions()
        {
            const int noPermissionsId = 22;
            _permissionsDataAccessorMock.Setup(x => x.HasPermissionForTask(UserId, noPermissionsId)).Returns(Task.FromResult(false));

            Action action = () =>
            {
                _taskDataProvider.UpdateStatusAsync(UserId, noPermissionsId, TaskStatus.Completed).Wait();
            };

            action.Should().Throw<NoPermissionsForOperationException>()
                .WithMessage(ErrorMessages.NoPermissionsForOperation);
        }
    }
}