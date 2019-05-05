using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Messages;
using TaskManager.Core.UserStorage;
using TaskManager.MessagingService.Data.Models;

namespace TaskManager.MessagingService.Data.DataProviders
{
    public class TasksHubUsersDataProvider : ITasksHubUsersDataProvider
    {
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;
        private readonly ITasksHubUsersStorage _storage;

        public TasksHubUsersDataProvider(IPermissionsDataAccessor permissionsDataAccessor, ITasksHubUsersStorage storage)
        {
            _permissionsDataAccessor = permissionsDataAccessor;
            _storage = storage;
        }

        public async Task Add(int userId, string connectionId)
        {
            _storage.Add(connectionId, new UserConnectionModel
            {
                UserId = userId,
                ProjectIds =  await _permissionsDataAccessor.Get().Where(x => x.UserId == userId).Select(x => x.ProjectId).Distinct().ToArrayAsync()
            });
        }

        public void Remove(string connectionId)
        {
            _storage.Remove(connectionId);
        }
    }
}