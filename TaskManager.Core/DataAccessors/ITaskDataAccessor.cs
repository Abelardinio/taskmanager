using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface ITaskDataAccessor
    {
        Task AddAsync(ITaskInfo task);
        IQueryable<ITask> Get(int userId, int? projectId);
        Task<ITask> UpdateStatusAsync(int taskId, TaskStatus status);
        Task AssignTaskAsync(int taskId, int? userId);
        Task<ITask> Get(int taskId);
    }
}