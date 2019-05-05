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

        public void StatusUpdated(int taskId, TaskStatus status, IProject project)
        {
            _client.SendMessage(EventLookup.TaskStatusUpdated,
                new TaskStatusUpdatedMessage
                    {TaskId = taskId, Status = status, ProjectId = project.Id, CreatorId = project.CreatorId});
        }

        public void OnStatusUpdated(Action<ITaskStatusUpdatedMessage> handler)
        {
           _client.Subscribe(EventLookup.TaskStatusUpdated, handler);
        }
    }
}