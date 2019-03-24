using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;

namespace TaskManager.Data.DataProviders
{
    public class ProjectsDataProvider : IProjectsDataProvider
    {
        private readonly IProjectsDataAccessor _projectsDataAccessor;

        public ProjectsDataProvider(IProjectsDataAccessor projectsDataAccessor)
        {
            _projectsDataAccessor = projectsDataAccessor;
        }

        public Task AddAsync(IProjectInfo info)
        {
            return _projectsDataAccessor.AddAsync(info);
        }

        public IQueryable<IProject> Get()
        {
            return _projectsDataAccessor.Get();
        }
    }
}