using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
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

        public IQueryable<IFeatureModel> Get()
        {
            return _contextStorage.Get().Features.Join(_contextStorage.Get().Projects, feature => feature.ProjectId, project => project.Id,
                (feature, project) => new FeatureModel
                {
                    Id = feature.Id,
                    ProjectName = project.Name,
                    Description = feature.Description,
                    Name = feature.Name
                });
        }
    }
}