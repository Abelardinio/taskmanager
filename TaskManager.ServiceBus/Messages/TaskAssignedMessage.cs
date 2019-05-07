using System;
using TaskManager.Core;
using TaskManager.Core.Messages;

namespace TaskManager.ServiceBus.Messages
{
    [Serializable]
    public class TaskAssignedMessage : ITaskAssignedMessage
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime TimeToComplete { get; set; }
        public DateTime Added { get; set; }
        public int? FeatureId { get; set; }
        public int AssignedUserId { get; set; }
        public int TaskId { get; set; }
        public TaskStatus Status { get; set; }
    }
}