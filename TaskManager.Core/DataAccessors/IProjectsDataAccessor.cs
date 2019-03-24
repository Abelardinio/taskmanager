using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface IProjectsDataAccessor
    {
        Task AddAsync(IProjectInfo task);
        IQueryable<IProject> Get();
    }
}