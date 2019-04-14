using TaskManager.Core.Enums;

namespace TaskManager.Core
{
    public interface IProjectPermission
    {
        int ProjectId { get; }
        Permission[] Permissions { get; }
    }
}