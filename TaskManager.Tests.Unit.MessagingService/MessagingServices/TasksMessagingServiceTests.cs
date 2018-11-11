using System;
using Moq;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.MessagingService;
using TaskManager.MessagingService.MessagingServices;
using TaskManager.ServiceBus.Messages;
using Xunit;

namespace TaskManager.Tests.Unit.MessagingService.MessagingServices
{
    public class TasksMessagingServiceTests
    {
        private readonly Mock<IHubClient<TasksHub>> _hubClientMock;
        private readonly Mock<IEventConnectionContext> _eventConnectionContextMock;
        private readonly Mock<ITaskEventAccessor> _taskEventAccessorMock;
        private readonly Mock<IHubContextAccessor> _hubContextAccessor;
        private readonly Mock<IEventScope> _eventScopeMock;
        private readonly IMessagingService _messagingService;

        public TasksMessagingServiceTests()
        {
            var resolverMock = new Mock<IDependencyResolver>();
            _eventConnectionContextMock = new Mock<IEventConnectionContext>();
            _taskEventAccessorMock = new Mock<ITaskEventAccessor>();
            _hubContextAccessor = new Mock<IHubContextAccessor>();
            _eventScopeMock = new Mock<IEventScope>();
            _hubClientMock = new Mock<IHubClient<TasksHub>>();

            _hubContextAccessor.SetupGet(x => x.TasksHubContext).Returns(_hubClientMock.Object);
            
            _eventConnectionContextMock.Setup(x => x.EventScope()).Returns(_eventScopeMock.Object);

            var resolvedObject = Tuple.Create(
                _eventConnectionContextMock.Object,
                _taskEventAccessorMock.Object,
                _hubContextAccessor.Object);

            resolverMock.Setup(x =>
                    x.Resolve<Tuple<IEventConnectionContext, ITaskEventAccessor, IHubContextAccessor>>())
                .Returns(resolvedObject);

            _messagingService = new TasksMessagingService(resolverMock.Object);
        }
        
        [Fact]
        public void ShouldInitEventScopeAndSubscribeToAccessorEventTest()
        {
            _messagingService.Start();
            _eventConnectionContextMock.Verify(x=>x.EventScope(),Times.Once);
            _taskEventAccessorMock.Verify(x=>x.OnStatusUpdated(It.IsAny<Action<ITaskStatusUpdatedMessage>>()), Times.Once);
        }

        [Fact]
        public void ShouldDisposeEventScopeOnServiceDisposeIfScopeWasInitializedTest()
        {
            _messagingService.Dispose();
            _eventScopeMock.Verify(x => x.Dispose(), Times.Never);
            _messagingService.Start();
            _messagingService.Dispose();
            _eventScopeMock.Verify(x =>x.Dispose(), Times.Once);
        }

        [Theory]
        [InlineData(TaskStatus.Completed, "TASK_COMPLETED")]
        [InlineData(TaskStatus.Removed, "TASK_DELETED")]
        public void TaskStatusUpdatedTest(TaskStatus status, string method)
        {
            const int taskId = 1;

            void Action(Action<ITaskStatusUpdatedMessage> handler)
            {
                var message = new TaskStatusUpdatedMessage {TaskId = taskId, Status = status};
                handler(message);
            }

            _taskEventAccessorMock.Setup(x => x.OnStatusUpdated(It.IsAny<Action<ITaskStatusUpdatedMessage>>()))
                .Callback((Action<Action<ITaskStatusUpdatedMessage>>) Action);

            _messagingService.Start();
            _hubClientMock.Verify(x => x.SendAsync(method, taskId), Times.Once);
        }

        [Theory]
        [InlineData(TaskStatus.Active)]
        [InlineData(TaskStatus.None)]
        public void TaskStatusUpdatedNoSignalRMessagesTest(TaskStatus status)
        {
            const int taskId = 1;

            void Action(Action<ITaskStatusUpdatedMessage> handler)
            {
                var message = new TaskStatusUpdatedMessage { TaskId = taskId, Status = status };
                handler(message);
            }

            _taskEventAccessorMock.Setup(x => x.OnStatusUpdated(It.IsAny<Action<ITaskStatusUpdatedMessage>>()))
                .Callback((Action<Action<ITaskStatusUpdatedMessage>>)Action);

            _messagingService.Start();
            _hubClientMock.Verify(x => x.SendAsync(It.IsAny<string>(), taskId), Times.Never);
        }
    }
}
