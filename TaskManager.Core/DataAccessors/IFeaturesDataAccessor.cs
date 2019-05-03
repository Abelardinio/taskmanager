using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.DataAccessors
{
    public interface IFeaturesDataAccessor
    {
        Task AddAsync(IFeatureInfo task);
        IQueryable<IFeatureModel> Get(int userId);
    }
}