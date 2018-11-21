using System;
using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class TaskInfo : ITaskInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime TimeToComplete { get; set; }
        public DateTime Added { get; set; }
    }
}
