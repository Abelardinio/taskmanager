using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.DbConnection.DataAccessors;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Enums;
using TaskManager.MessagingService.DbConnection.Entities;

namespace TaskManager.MessagingService.DbConnection.DataAccessors
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

        public IQueryable<IUserProjectPermission> Get()
        {
            return _contextStorage.Get().Permissions;
        }

        public Task<bool> HasPermission(int userId, int projectId, Permission permission)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPermissionForFeature(int userId, int featureId, Permission perm)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPermissionForTask(int userId, int taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}