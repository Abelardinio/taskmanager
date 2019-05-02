using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.DbConnection.DataAccessors;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Enums;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection.DataAccessors
{
    public class PermissionsDataAccessor : PermissionsDataAccessorBase<UserPermissionEntity>, IPermissionsDataAccessor
    {
        private readonly IContextStorage _contextStorage;

        public PermissionsDataAccessor(IContextStorage contextStorage)
        {
            _contextStorage = contextStorage;
        }

        protected override DbSet<UserPermissionEntity> Permissions => _contextStorage.Get().Permissions;
        protected override Task SaveChangesAsync()
        {
            return _contextStorage.Get().SaveChangesAsync();
        }

        protected override UserPermissionEntity CreateEntity(int userId, int projectId, Permission permission)
        {
            return new UserPermissionEntity
            {
                UserId = userId,
                ProjectId = projectId,
                Permission = permission
            };
        }
    }
}