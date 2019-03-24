using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddAsync(IProjectInfo info)
        {
            var context = _contextStorage.Get();

            context.Projects.Add(new ProjectEntity(info));
            await context.SaveChangesAsync();
        }

        public IQueryable<IProject> Get()
        {
           return _contextStorage.Get().Projects;
        }
    }
}