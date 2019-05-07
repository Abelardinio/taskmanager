namespace TaskManager.Core.Messages
{
    public interface ITaskUnassignedMessage
    {
        int UserId { get; }

        int TaskId { get; }
    }
}