using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DataProviders
{
    public interface IPermissionsDataProvider
    {
        Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions);
        IQueryable<Permission> Get(int userId, int projectId);
    }
}