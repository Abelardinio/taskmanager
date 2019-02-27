using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;

namespace TaskManager.AuthService.Data.DataProviders
{
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly IUsersDataAccessor _usersDataAccessor;

        public UsersDataProvider(IUsersDataAccessor usersDataAccessor)
        {
            _usersDataAccessor = usersDataAccessor;
        }

        public Task AddAsync(IUserInfo user)
        {
            return _usersDataAccessor.AddAsync(user);
        }
    }
}