using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ITaskDataProvider
    {
        Task Add(ITaskInfo task);
        Task<IReadOnlyList<ITask>> Get();
        Task<ITask> Get(int taskId);
    }
}