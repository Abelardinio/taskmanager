using TaskManager.Core.Enums;

namespace TaskManager.Common.DbConnection.Entities
{
    public interface IUserPermissionEntity
    {
        int Id { get; }
        int UserId { get; }
        Permission Permission { get; }
        int ProjectId { get; }
    }
}