namespace TaskManager.Core.Messages
{
    public interface ITaskStatusUpdatedMessage
    {
        int TaskId { get; }

        TaskStatus Status { get; }
    }
}