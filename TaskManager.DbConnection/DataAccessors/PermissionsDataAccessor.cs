using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.DbConnection.DataAccessors;
using TaskManager.Core;
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

        public IQueryable<IUserProjectPermission> Get()
        {
            return _contextStorage.Get().Permissions;
        }

        public Task<bool> HasPermission(int userId, int projectId, Permission permission)
        {
            return _contextStorage.Get().Permissions.AnyAsync(x =>
                x.UserId == userId && x.ProjectId == projectId &&
                (x.Permission == permission || x.Permission == Permission.Admin));
        }

        public Task<bool> HasPermissionForFeature(int userId, int featureId, Permission perm)
        {
            return _contextStorage.Get().Permissions.Join(
                _contextStorage.Get().Features,
                permission => permission.ProjectId,
                feature => feature.ProjectId,
                (permission, feature) => new {permission, feature}).AnyAsync(x =>
                x.permission.UserId == userId && x.feature.Id == featureId &&
                (x.permission.Permission == perm || x.permission.Permission == Permission.Admin));
        }

        public Task<bool> HasPermissionForTask(int userId, int taskId)
        {
            return _contextStorage.Get().Tasks
                .Join(_contextStorage.Get().Features, task => task.FeatureId,
                    feature => feature.Id, (task, feature) => new {task, feature})
                .Join(_contextStorage.Get().Permissions,
                    taskFeature => taskFeature.feature.ProjectId, permission => permission.ProjectId,
                    (taskFeature, permission) => new
                    {
                        taskFeature.task,
                        taskFeature.feature,
                        permission
                    }).AnyAsync(x => x.task.Id == taskId && x.permission.UserId == userId);
        }
    }
}