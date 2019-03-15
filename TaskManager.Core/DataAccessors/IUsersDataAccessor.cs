using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface IUsersDataAccessor
    {
        Task AddAsync(IUserInfo task);

        IQueryable<IUser> Get();

        Task SetPasswordAsync(int userId, string password);
    }
}