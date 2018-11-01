using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface ITaskDataAccessor
    {
        Task AddAsync(ITaskInfo task);
        IQueryable<ITask> Get();
        Task UpdateStatusAsync(int taskId, TaskStatus status);
    }
}