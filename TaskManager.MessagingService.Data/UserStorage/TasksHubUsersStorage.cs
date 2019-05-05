using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Core;
using TaskManager.Core.UserStorage;

namespace TaskManager.MessagingService.Data.UserStorage
{
    public class TasksHubUsersStorage : ITasksHubUsersStorage
    {
        private static readonly Dictionary<string, IUserConnectionModel> _dictionary = new Dictionary<string, IUserConnectionModel>();
        public void Add(string connectionId, IUserConnectionModel model)
        {
            _dictionary[connectionId] = model;
        }

        public void Remove(string connectionId)
        {
            _dictionary.Remove(connectionId);
        }

        public string[] Get(int projectId, int creatorId)
        {
            return _dictionary.Where(x => x.Value.ProjectIds.Any(y => y == projectId) || x.Value.UserId == creatorId)
                .Select(x => x.Key).ToArray();
        }
    }
}