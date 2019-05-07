using System;
using TaskManager.Core;

namespace TaskManager.HomeService.DbConnection.Entities
{
    public class TaskEntity : IUserTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime TimeToComplete { get; set; }
        public DateTime Added { get; set; }
        public int? FeatureId { get; set; }
        public int Id { get; set; }
        public TaskStatus Status { get; set; }
    }
}