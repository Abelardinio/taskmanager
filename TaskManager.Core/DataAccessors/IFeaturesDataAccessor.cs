using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DataAccessors
{
    public interface IFeaturesDataAccessor
    {
        Task AddAsync(IFeatureInfo task);
        IQueryable<IFeatureModel> Get(int userId, Permission? permission = null);
    }
}