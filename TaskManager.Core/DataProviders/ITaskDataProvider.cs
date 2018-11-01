using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ITaskDataProvider
    {
        Task AddAsync(ITaskInfo task);
        IQueryable<ITask> GetLiveTasks();
        Task UpdateStatusAsync(int taskId, TaskStatus status);
    }
}