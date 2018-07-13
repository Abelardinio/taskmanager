using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface ITaskDataAccessor
    {
        Task Add(ITaskInfo task);
        IQueryable<ITask> Get();
        Task UpdateStatus(int taskId, TaskStatus status);
    }
}