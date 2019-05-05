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

        public async Task<IProject> GetAsync(int taskId)
        {
            return await _contextStorage.Get().Tasks
                .Join(_contextStorage.Get().Features, task => task.FeatureId,
                    feature => feature.Id, (task, feature) => new {task, feature})
                .Join(_contextStorage.Get().Projects,
                taskFeature => taskFeature.feature.ProjectId, project => project.Id, (taskFeature, project) => new
                {
                    taskFeature.task,
                    taskFeature.feature,
                    project
                }).Where(x => x.task.Id == taskId).Select(x => x.project).FirstOrDefaultAsync();
        }

        public Task<bool> IsProjectCreator(int userId, int featureId)
        {
            return _contextStorage.Get().Projects
                .Join(_contextStorage.Get().Features, project => project.Id, feature => feature.ProjectId,
                    (project, feature) => new {feature, project})
                .AnyAsync(x => x.feature.Id == featureId && x.project.CreatorId == userId);
        }

        public Task<bool> IsProjectCreatorByTaskId(int userId, int taskId)
        {
            return _contextStorage.Get().Projects
                .Join(_contextStorage.Get().Features, project => project.Id, feature => feature.ProjectId,
                    (project, feature) => new {feature, project})
                .Join(_contextStorage.Get().Tasks, projectFeature => projectFeature.feature.Id, task => task.FeatureId,
                    (projectFeature, task) => new
                    {
                        projectFeature.feature,
                        projectFeature.project,
                        task
                    })
                .AnyAsync(x => x.task.Id == taskId && x.project.CreatorId == userId);
        }
    }
}