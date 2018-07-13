using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class TaskFilter : ITaskFilter
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}