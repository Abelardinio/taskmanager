using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;

namespace TaskManager.Data.DataProviders
{
    public class FeaturesDataProvider : IFeaturesDataProvider
    {
        private readonly IFeaturesDataAccessor _featuresDataAccessor;

        public FeaturesDataProvider(IFeaturesDataAccessor featuresDataAccessor)
        {
            _featuresDataAccessor = featuresDataAccessor;
        }

        public Task AddAsync(IFeatureInfo info)
        {
            return _featuresDataAccessor.AddAsync(info);
        }

        public IQueryable<IFeature> Get()
        {
            return _featuresDataAccessor.Get();
        }
    }
}