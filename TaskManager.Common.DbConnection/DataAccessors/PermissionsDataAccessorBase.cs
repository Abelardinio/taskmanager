using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.DbConnection.Entities;
using TaskManager.Core;
using TaskManager.Core.Enums;

namespace TaskManager.Common.DbConnection.DataAccessors
{
    public abstract class PermissionsDataAccessorBase<T> where T : class, IUserPermissionEntity
    {
        protected abstract DbSet<T> Permissions { get; }

        protected abstract Task SaveChangesAsync();

        protected abstract T CreateEntity(int userId, int projectId, Permission permission);

        public async Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions)
        {
            Permissions.RemoveRange(Permissions.Where(x =>
                permissions.Any(y => y.ProjectId == x.ProjectId && x.UserId == userId)));

            foreach (var projectPermission in permissions)
            {
                foreach (var permission in projectPermission.Permissions)
                {
                    Permissions.Add(CreateEntity(userId, projectPermission.ProjectId, permission));
                }
            }

            await SaveChangesAsync();
        }

        public IQueryable<Permission> Get(int userId, int projectId)
        {
            return Permissions
                .Where(x => x.UserId == userId && x.ProjectId == projectId)
                .Select(x => x.Permission);
        }
    }
}
