using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Enums;

namespace TaskManager.Data.DataProviders
{
    public class ProjectsDataProvider : IProjectsDataProvider
    {
        private readonly IProjectsDataAccessor _projectsDataAccessor;
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;

        public ProjectsDataProvider(IProjectsDataAccessor projectsDataAccessor, IPermissionsDataAccessor permissionsDataAccessor)
        {
            _projectsDataAccessor = projectsDataAccessor;
            _permissionsDataAccessor = permissionsDataAccessor;
        }

        public Task AddAsync(int userId, IProjectInfo info)
        {
            return _projectsDataAccessor.AddAsync(userId, info);
        }

        public IQueryable<IProject> Get(int userId, Permission? permission = null)
        {
            return _projectsDataAccessor.Get().Where(x => x.CreatorId == userId ||
                                                          _permissionsDataAccessor.Get().Any(y =>
                                                              y.ProjectId == x.Id && y.UserId == userId &&
                                                              (!permission.HasValue || permission == y.Permission ||
                                                               y.Permission == Permission.Admin)));
        }
    }
}