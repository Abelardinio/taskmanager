using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ITaskDataProvider
    {
        Task Add(ITaskInfo task);
        IQueryable<ITask> GetUnremoved();
        Task UpdateStatusAsync(int taskId, TaskStatus status);
    }
}