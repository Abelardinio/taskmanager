namespace TaskManager.Core.Messages
{
    public interface IPermissionsUpdatedMessage
    {
        int UserId { get; }
        IProjectPermission[] Permissions { get; }
    }
}