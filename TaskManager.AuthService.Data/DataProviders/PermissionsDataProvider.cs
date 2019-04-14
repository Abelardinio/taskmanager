using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.Data.DataProviders
{
    public class PermissionsDataProvider : IPermissionsDataProvider
    {
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;

        public PermissionsDataProvider(IPermissionsDataAccessor permissionsDataAccessor)
        {
            _permissionsDataAccessor = permissionsDataAccessor;
        }

        public Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions)
        {
            return _permissionsDataAccessor.UpdatePermissionsForUserAsync(userId, permissions);
        }

        public IQueryable<Permission> Get(int userId, int projectId)
        {
            return _permissionsDataAccessor.Get(userId, projectId);
        }
    }
}