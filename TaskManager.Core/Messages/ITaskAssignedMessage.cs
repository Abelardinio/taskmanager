namespace TaskManager.Core.Messages
{
    public interface ITaskAssignedMessage : ITaskInfo
    {
        int AssignedUserId { get; }
        int TaskId { get; }
        TaskStatus Status { get; }
    }
}