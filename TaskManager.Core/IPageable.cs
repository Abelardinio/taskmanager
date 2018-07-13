namespace TaskManager.Core
{
    public interface IPageable
    {
        int Skip { get; }
        int Take { get; }
    }
}