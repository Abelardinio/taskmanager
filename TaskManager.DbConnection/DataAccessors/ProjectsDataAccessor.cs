using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection.DataAccessors
{
    public class ProjectsDataAccessor : IProjectsDataAccessor
    {
        private readonly IContextStorage _contextStorage;

        public ProjectsDataAccessor(IContextStorage contextStorage)
        {
            _contextStorage = contextStorage;
        }

        public async Task AddAsync(int userId, IProjectInfo info)
        {
            var context = _contextStorage.Get();

            context.Projects.Add(new ProjectEntity(userId, info));
            await context.SaveChangesAsync();
        }

        public IQueryable<IProject> Get()
        {
           return _contextStorage.Get().Projects;
        }

        public Task<bool> IsProjectCreator(int userId, int featureId)
        {
            return _contextStorage.Get().Projects
                .Join(_contextStorage.Get().Features, project => project.Id, feature => feature.ProjectId,
                    (project, feature) => new {feature, project})
                .AnyAsync(x => x.feature.Id == featureId && x.project.CreatorId == userId);
        }
    }
}