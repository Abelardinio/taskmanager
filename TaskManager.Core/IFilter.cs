namespace TaskManager.Core
{
    public interface IFilter<T> : ISortable<T>, IFilterable<T>, IPageable<T>
    {
    }
}