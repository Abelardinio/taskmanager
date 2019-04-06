using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.AuthService.DbConnection.Entities;
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

        public async Task SetPasswordAsync(int userId, string password)
        {
            var user  = await _contextStorage.Get().Users.SingleOrDefaultAsync(x => x.Id == userId);

            user.Password = password;
            await _contextStorage.Get().SaveChangesAsync();
        }
    }
}