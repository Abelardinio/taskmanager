using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;

namespace TaskManager.HomeService.Data.DataProviders
{
    public class UserTasksDataProvider : IUserTasksDataProvider
    {
        private readonly IUserTasksDataAccessor _userTasksDataAccessor;

        public UserTasksDataProvider(IUserTasksDataAccessor userTasksDataAccessor)
        {
            _userTasksDataAccessor = userTasksDataAccessor;
        }

        public Task<IReadOnlyList<IUserTask>> Get(int userId)
        {
            return _userTasksDataAccessor.Get(userId);
        }
    }
}