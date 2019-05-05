using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface IProjectsDataAccessor
    {
        Task AddAsync(int userId, IProjectInfo task);
        IQueryable<IProject> Get();
        Task<IProject> GetAsync(int taskId);
        Task<bool> IsProjectCreator(int userId, int featureId);
    }
}