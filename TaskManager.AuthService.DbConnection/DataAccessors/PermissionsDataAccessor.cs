using System.Linq;
using System.Threading.Tasks;
using TaskManager.AuthService.DbConnection.Entities;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.DbConnection.DataAccessors
{
    public class PermissionsDataAccessor : IPermissionsDataAccessor
    {
        private readonly IContextStorage _contextStorage;

        public PermissionsDataAccessor(IContextStorage contextStorage)
        {
            _contextStorage = contextStorage;
        }

        public async Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions)
        {
            var context = _contextStorage.Get();

            context.UserPermissions.RemoveRange(context.UserPermissions.Where(x =>
                permissions.Any(y => y.ProjectId == x.ProjectId && x.UserId == userId)));

            foreach (var projectPermission in permissions)
            {
                foreach (var permission in projectPermission.Permissions)
                {
                    context.UserPermissions.Add(new UserPermissionEntity
                    {
                        Permission = permission,
                        ProjectId = projectPermission.ProjectId,
                        UserId = userId
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public IQueryable<Permission> Get(int userId, int projectId)
        {
            return _contextStorage.Get().UserPermissions
                .Where(x => x.UserId == userId && x.ProjectId == projectId)
                .Select(x => x.Permission);
        }
    }
}