namespace TaskManager.Core
{
    public interface IPermissionsUpdatedMessage
    {
        int UserId { get; }
        IProjectPermission[] Permissions { get; }
    }
}