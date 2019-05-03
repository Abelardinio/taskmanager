using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Enums;
using TaskManager.DbConnection.Entities;
using TaskManager.DbConnection.Models;

namespace TaskManager.DbConnection.DataAccessors
{
    public class FeaturesDataAccessor : IFeaturesDataAccessor
    {
        private readonly IContextStorage _contextStorage;

        public FeaturesDataAccessor(IContextStorage contextStorage)
        {
            _contextStorage = contextStorage;
        }

        public async Task AddAsync(IFeatureInfo info)
        {
            var context = _contextStorage.Get();

            context.Features.Add(new FeatureEntity(info));
            await context.SaveChangesAsync();
        }

        public IQueryable<IFeatureModel> Get(int userId, Permission? permission = null)
        {
            return _contextStorage.Get().Features.Join(_contextStorage.Get().Projects, feature => feature.ProjectId,
                project => project.Id,
                (feature, project) => new
                {
                    feature, project
                }).Where(x => x.project.CreatorId == userId || _contextStorage.Get().Permissions
                                  .Any(y => y.UserId == userId && x.project.Id == y.ProjectId &&
                                            (!permission.HasValue || y.Permission == permission.Value ||
                                             y.Permission == Permission.Admin))).Select(x =>
                new FeatureModel
                {
                    Id = x.feature.Id,
                    Description = x.feature.Description,
                    Name = x.feature.Name,
                    ProjectId = x.project.Id,
                    ProjectName = x.project.Name
                });
        }
    }
}