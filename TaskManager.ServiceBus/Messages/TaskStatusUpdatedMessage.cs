using System;
using TaskManager.Core;
using TaskManager.Core.Messages;

namespace TaskManager.ServiceBus.Messages
{
    [Serializable]
    public class TaskStatusUpdatedMessage : ITaskStatusUpdatedMessage
    {
        public int TaskId { get; set; }

        public TaskStatus Status { get; set; }
    }
}