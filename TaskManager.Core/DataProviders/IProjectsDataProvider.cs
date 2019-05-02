using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DataProviders
{
    public interface IProjectsDataProvider
    {
        Task AddAsync(int userId, IProjectInfo info);
        IQueryable<IProject> Get(int userId, Permission? permission = null);
    }
}