using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ITaskDataProvider
    {
        Task Add(ITaskInfo task);
        IQueryable<ITask> Get(ITaskFilter filter);
        Task<ITask> Get(int taskId);
        Task UpdateStatus(int taskId, TaskStatus status);
    }
}