using System;
using TaskManager.Core.Messages;

namespace TaskManager.ServiceBus.Messages
{
    [Serializable]
    public class TaskUnassignedMessage : ITaskUnassignedMessage
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}