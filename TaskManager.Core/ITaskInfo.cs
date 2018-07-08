using System;

namespace TaskManager.Core
{
    public interface ITaskInfo
    {
        string Name { get; }
        string Description { get; }
        int Priority { get; }
        DateTime TimeToComplete { get; }
        DateTime Added { get; }
    }
}