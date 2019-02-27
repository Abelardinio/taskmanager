using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface IUsersDataProvider
    {
        Task AddAsync(IUserInfo user);
    }
}