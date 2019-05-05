using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Moq;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.Core.UserStorage;
using TaskManager.MessagingService.Data;
using TaskManager.MessagingService.WebApi;
using TaskManager.MessagingService.WebApi.MessagingServices;
using TaskManager.ServiceBus.Messages;
using Xunit;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Tests.Unit.MessagingService.MessagingServices
{
    public class TasksMessagingServiceTests
    {
        private const int CreatorId = 8;
        private const int ProjectId = 9;
        private readonly string[] _connectionIds = {"connectionId"};
        private readonly Mock<IHubClient<TasksHub>> _hubClientMock;
        private readonly Mock<IEventConnectionContext> _eventConnectionContextMock;
        private readonly Mock<ITaskEventAccessor> _taskEventAccessorMock;
        private readonly Mock<IEventScope> _eventScopeMock;
        private readonly Mock<ITasksHubUsersStorage> _tasksHubStorage;
        private readonly IHostedService _messagingService;

        public TasksMessagingServiceTests()
        {
            _eventConnectionContextMock = new Mock<IEventConnectionContext>();
            _taskEventAccessorMock = new Mock<ITaskEventAccessor>();
            _eventScopeMock = new Mock<IEventScope>();
            _tasksHubStorage =  new Mock<ITasksHubUsersStorage>();
            _hubClientMock = new Mock<IHubClient<TasksHub>>();

            _eventConnectionContextMock.Setup(x => x.EventScope()).Returns(_eventScopeMock.Object);
            _tasksHubStorage.Setup(x => x.Get(ProjectId, CreatorId)).Returns(_connectionIds);

            _messagingService = new TasksMessagingService(_eventConnectionContextMock.Object,
                _taskEventAccessorMock.Object, _hubClientMock.Object, _tasksHubStorage.Object);
        }

        [Fact]
        public void ShouldInitEventScopeAndSubscribeToAccessorEventTest()
        {
            _messagingService.StartAsync(CancellationToken.None);
            _eventConnectionContextMock.Verify(x => x.EventScope(), Times.Once);
            _taskEventAccessorMock.Verify(x => x.OnStatusUpdated(It.IsAny<Action<ITaskStatusUpdatedMessage>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldDisposeEventScopeOnServiceDisposeIfScopeWasInitializedTest()
        {
            await _messagingService.StopAsync(CancellationToken.None);
            _eventScopeMock.Verify(x => x.Dispose(), Times.Never);
            await _messagingService.StartAsync(CancellationToken.None);
            await _messagingService.StopAsync(CancellationToken.None);
            _eventScopeMock.Verify(x => x.Dispose(), Times.Once);
        }

        [Theory]
        [InlineData(TaskStatus.Completed, "TASK_COMPLETED")]
        [InlineData(TaskStatus.Removed, "TASK_DELETED")]
        public async Task TaskStatusUpdatedTest(TaskStatus status, string method)
        {
            const int taskId = 1;

            void Action(Action<ITaskStatusUpdatedMessage> handler)
            {
                var message = new TaskStatusUpdatedMessage { TaskId = taskId, Status = status, CreatorId = CreatorId, ProjectId = ProjectId };
                handler(message);
            }

            _taskEventAccessorMock.Setup(x => x.OnStatusUpdated(It.IsAny<Action<ITaskStatusUpdatedMessage>>()))
                .Callback((Action<Action<ITaskStatusUpdatedMessage>>)Action);

            await _messagingService.StartAsync(CancellationToken.None);
            _hubClientMock.Verify(x => x.SendAsync(method, taskId, _connectionIds), Times.Once);
        }

        [Theory]
        [InlineData(TaskStatus.Active)]
        [InlineData(TaskStatus.None)]
        public async Task TaskStatusUpdatedNoSignalRMessagesTest(TaskStatus status)
        {
            const int taskId = 1;

            void Action(Action<ITaskStatusUpdatedMessage> handler)
            {
                var message = new TaskStatusUpdatedMessage { TaskId = taskId, Status = status, CreatorId = CreatorId, ProjectId = ProjectId };
                handler(message);
            }

            _taskEventAccessorMock.Setup(x => x.OnStatusUpdated(It.IsAny<Action<ITaskStatusUpdatedMessage>>()))
                .Callback((Action<Action<ITaskStatusUpdatedMessage>>)Action);

            await _messagingService.StartAsync(CancellationToken.None);
            _hubClientMock.Verify(x => x.SendAsync(It.IsAny<string>(), taskId, _connectionIds), Times.Never);
        }
    }
}
