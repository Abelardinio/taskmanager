using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ITaskDataProvider
    {
        Task Add(ITaskInfo task);
        IQueryable<ITask> GetUnremoved();
        Task<ITask> Get(int taskId);
        Task UpdateStatus(int taskId, TaskStatus status);
    }
}