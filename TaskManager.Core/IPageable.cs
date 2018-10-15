namespace TaskManager.Core
{
    public interface IPageable<T>
    {
        int PageSize { get; }
        int PageNumber { get; }
    }
}