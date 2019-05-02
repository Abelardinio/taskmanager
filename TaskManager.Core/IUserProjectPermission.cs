using TaskManager.Core.Enums;

namespace TaskManager.Core
{
    public interface IUserProjectPermission
    {
        int UserId { get; }
        Permission Permission { get; }
        int ProjectId { get; }
    }
}