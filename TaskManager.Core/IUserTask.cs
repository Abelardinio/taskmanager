using System;

namespace TaskManager.Core
{
    public interface IUserTask
    {
        string Name { get; set; }
        string Description { get; set; }
        int Priority { get; set; }
        DateTime TimeToComplete { get; set; }
        DateTime Added { get; set; }
        int? FeatureId { get; set; }
        int Id { get; set; }
        TaskStatus Status { get; set; }
    }
}