using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface IUserTasksDataAccessor
    {
        Task AddTaskToUser(int userId, int taskId, TaskStatus status, ITaskInfo taskInfo);
        Task RemoveTask(int userId, int taskId);
        Task ChangeTaskStatus(int userId, int taskId, TaskStatus status);
    }
}