using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.DbConnection.Entities;

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

        public IQueryable<IFeature> Get()
        {
            return _contextStorage.Get().Features;
        }
    }
}