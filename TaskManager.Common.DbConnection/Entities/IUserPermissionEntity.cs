using TaskManager.Core;

namespace TaskManager.Common.DbConnection.Entities
{
    public interface IUserPermissionEntity : IUserProjectPermission
    {
        int Id { get; }
    }
}