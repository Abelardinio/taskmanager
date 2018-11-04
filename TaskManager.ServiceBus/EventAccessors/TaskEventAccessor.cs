using System;
using TaskManager.Core;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.ServiceBus.Messages;

namespace TaskManager.ServiceBus.EventAccessors
{
    public class TaskEventAccessor : ITaskEventAccessor
    {
        private readonly IServiceBusClient _client;

        public TaskEventAccessor(IServiceBusClient client)
        {
            _client = client;
        }

        public void StatusUpdated(int taskId, TaskStatus status)
        {
            _client.SendMessage(QueueNumber.TaskStatusUpdated,
                new TaskStatusUpdatedMessage {TaskId = taskId, Status = status});
        }

        public void OnStatusUpdated(Action<ITaskStatusUpdatedMessage> handler)
        {
           _client.Subscribe(QueueNumber.TaskStatusUpdated, handler);
        }
    }
}