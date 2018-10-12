using System.Linq;

namespace TaskManager.Core
{
    public interface IFilterable<T>
    {
        IQueryable<T> Filter(IQueryable<T> input);
    }
}