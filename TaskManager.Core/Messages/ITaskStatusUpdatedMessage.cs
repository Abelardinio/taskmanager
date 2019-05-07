namespace TaskManager.Core.Messages
{
    public interface ITaskStatusUpdatedMessage
    {
        int TaskId { get; }

        int? AssignedUserId { get; }

        TaskStatus Status { get; }

        int ProjectId { get; }

        int CreatorId { get; }
    }
}