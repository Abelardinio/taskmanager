using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface ITaskDataAccessor
    {
        Task Add(ITaskInfo task);
        Task<IReadOnlyList<ITask>> Get(ITaskFilter filter);
        Task<ITask> Get(int taskId);
    }
}