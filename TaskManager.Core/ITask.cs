namespace TaskManager.Core
{
    public interface ITask : ITaskInfo
    {
        int Id { get; }
        TaskStatus Status { get; }
        int? AssignedUserId { get; }
    }
}