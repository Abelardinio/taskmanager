using System.Collections.Generic;

namespace TaskManager.Core.UserStorage
{
    public interface ITasksHubUsersStorage
    {
        void Add(string connectionId, IUserConnectionModel model);

        void Remove(string connectionId);

        string[] Get(int projectId, int creatorId);

        string[] Get(int userId);
    }
}