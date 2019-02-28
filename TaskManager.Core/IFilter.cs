namespace TaskManager.Core
{
    public interface IFilter<T> : ISortable<T>, IUnsortableFilter<T>
    {
    }
}