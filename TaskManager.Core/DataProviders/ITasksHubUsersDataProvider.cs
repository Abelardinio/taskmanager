using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ITasksHubUsersDataProvider
    {
        Task Add(int userId, string connectionId);
        void Remove(string connectionId);
    }
}