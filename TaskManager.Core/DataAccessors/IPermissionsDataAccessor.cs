using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DataAccessors
{
    public interface IPermissionsDataAccessor
    {
        Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions);

        IQueryable<Permission> Get(int userId, int projectId);

        IQueryable<IUserProjectPermission> Get();

        Task<bool> HasPermission(int userId, int projectId, Permission permission);

        Task<bool> HasPermissionForFeature(int userId, int featureId, Permission perm);
    }
}