using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.AuthService.DbConnection.Entities;
using TaskManager.AuthService.DbConnection.Models;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;

namespace TaskManager.AuthService.DbConnection.DataAccessors
{
    public class UsersDataAccessor : IUsersDataAccessor
    {
        private readonly IContextStorage _contextStorage;

        public UsersDataAccessor(IContextStorage contextStorage)
        {
            _contextStorage = contextStorage;
        }

        public Task AddAsync(IUserInfo user)
        {
            var context = _contextStorage.Get();

            context.Users.Add(new UserEntity(user));

            return context.SaveChangesAsync();
        }

        public IQueryable<IUser> Get()
        {
            return _contextStorage.Get().Users;
        }

        public IQueryable<IUserLoginInfo> GetLoginInfo()
        {
            return _contextStorage.Get().Users.Join(_contextStorage.Get().UserRoles, user => user.Id,
                userRole => userRole.UserId, (user, userRole) => new UserLoginInfoModel
                {
                    Id = user.Id,
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt,
                    Role = userRole.Role,
                    Username = user.Username
                });
        }

        public async Task SetPasswordAsync(int userId, string password)
        {
            var user  = await _contextStorage.Get().Users.SingleOrDefaultAsync(x => x.Id == userId);

            user.Password = password;
            await _contextStorage.Get().SaveChangesAsync();
        }
    }
}