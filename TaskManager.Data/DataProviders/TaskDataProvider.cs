using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Data.DataProviders
{
    public class TaskDataProvider : ITaskDataProvider
    {
        private readonly ITaskDataAccessor _taskDataAccessor;

        public TaskDataProvider(ITaskDataAccessor taskDataAccessor)
        {
            _taskDataAccessor = taskDataAccessor;
        }

        public Task AddAsync(ITaskInfo task)
        {
            return _taskDataAccessor.AddAsync(task);
        }

        public IQueryable<ITask> GetLiveTasks()
        {
            return _taskDataAccessor.Get().Where(x => x.Status != TaskStatus.Removed && x.Status != TaskStatus.None);
        }

        public Task UpdateStatusAsync(int taskId, TaskStatus status)
        {
            return _taskDataAccessor.UpdateStatusAsync(taskId, status);
        }
    }
}