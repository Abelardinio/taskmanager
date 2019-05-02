using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Enums;
using TaskManager.Core.EventAccessors;

namespace TaskManager.AuthService.Data.DataProviders
{
    public class PermissionsDataProvider : IPermissionsDataProvider
    {
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;
        private readonly IPermissionsEventAccessor _permissionsEventAccessor;
        private readonly IConnectionContext _connectionContext;

        public PermissionsDataProvider(
            IPermissionsDataAccessor permissionsDataAccessor,
            IPermissionsEventAccessor permissionsEventAccessor,
            IConnectionContext connectionContext)
        {
            _permissionsDataAccessor = permissionsDataAccessor;
            _permissionsEventAccessor = permissionsEventAccessor;
            _connectionContext = connectionContext;
        }

        public async Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions)
        {
            await _permissionsDataAccessor.UpdatePermissionsForUserAsync(userId, permissions);

            using (_connectionContext.EventScope())
            {
                _permissionsEventAccessor.PermissionsUpdated(userId, permissions);
            }
        }

        public IQueryable<Permission> Get(int userId, int projectId)
        {
            return _permissionsDataAccessor.Get(userId, projectId);
        }
    }
}