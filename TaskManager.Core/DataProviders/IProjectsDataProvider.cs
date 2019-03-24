using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface IProjectsDataProvider
    {
        Task AddAsync(IProjectInfo info);
        IQueryable<IProject> Get();
    }
}