using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class TaskFilter : ITaskFilter
    {
        public int TaskId { get; set; }
        public int Count { get; set; }
        public TakeType Type { get; set; }
    }
}