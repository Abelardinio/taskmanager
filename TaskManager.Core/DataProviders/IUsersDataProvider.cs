using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface IUsersDataProvider
    {
        Task AddAsync(IUserInfo user);
        IQueryable<IUser> Get();
    }
}