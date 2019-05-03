using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface IFeaturesDataProvider
    {
        Task AddAsync(int userId, IFeatureInfo info);
        IQueryable<IFeatureModel> Get(int userId);
    }
}