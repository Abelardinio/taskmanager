using System;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core;

namespace TaskManager.DbConnection.Entities
{
    [Table("Tasks")]
    internal class TaskEntity : ITask
    {
        public TaskEntity(){}

        public TaskEntity(ITaskInfo taskInfo)
        {
            Name = taskInfo.Name;
            Description = taskInfo.Description;
            Priority = taskInfo.Priority;
            TimeToComplete = taskInfo.TimeToComplete;
            Added = taskInfo.Added;
            Status = TaskStatus.Active;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime TimeToComplete { get; set; }
        public DateTime Added { get; set; }
        public TaskStatus Status { get; set; }
    }
}
