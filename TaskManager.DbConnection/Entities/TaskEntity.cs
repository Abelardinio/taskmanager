using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core;

namespace TaskManager.DbConnection.Entities
{
    [Table("Tasks")]
    public class TaskEntity : ITask
    {
        public TaskEntity(){}

        public TaskEntity(ITaskInfo taskInfo)
        {
            Name = taskInfo.Name;
            Description = taskInfo.Description;
            Priority = taskInfo.Priority;
            TimeToComplete = taskInfo.TimeToComplete;
            Added = taskInfo.Added;
            FeatureId = taskInfo.FeatureId;
            Status = TaskStatus.Active;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime TimeToComplete { get; set; }
        public DateTime Added { get; set; }
        public int? FeatureId { get; set; }
        public int? AssignedUserId { get; set; }
        public FeatureEntity Feature { get; set; }
        public TaskStatus Status { get; set; }
    }
}
