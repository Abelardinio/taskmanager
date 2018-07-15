namespace TaskManager.Core
{
    public interface ITaskFilter 
    {
        int TaskId { get; }

        int Count { get; }

        TakeType Type { get; }
    }
}