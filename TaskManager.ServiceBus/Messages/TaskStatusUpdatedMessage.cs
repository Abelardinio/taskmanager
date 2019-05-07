using System;
using TaskManager.Core;
using TaskManager.Core.Messages;

namespace TaskManager.ServiceBus.Messages
{
    [Serializable]
    public class TaskStatusUpdatedMessage : ITaskStatusUpdatedMessage
    {
        public int TaskId { get; set; }
        public int? AssignedUserId { get; set; }
        public TaskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public int CreatorId { get; set; }
    }
}