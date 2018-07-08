using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;

namespace TaskManager.Data.DataProviders
{
    public class TaskDataProvider : ITaskDataProvider
    {
        private readonly ITaskDataAccessor _taskDataAccessor;

        public TaskDataProvider(ITaskDataAccessor taskDataAccessor)
        {
            _taskDataAccessor = taskDataAccessor;
        }

        public Task Add(ITaskInfo task)
        {
            return _taskDataAccessor.Add(task);
        }

        public Task<IReadOnlyList<ITask>> Get()
        {
            return _taskDataAccessor.Get();
        }

        public Task<ITask> Get(int taskId)
        {
            return _taskDataAccessor.Get(taskId);
        }
    }
}