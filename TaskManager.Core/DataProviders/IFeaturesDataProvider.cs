using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface IFeaturesDataProvider
    {
        Task AddAsync(IFeatureInfo info);
        IQueryable<IFeature> Get();
    }
}