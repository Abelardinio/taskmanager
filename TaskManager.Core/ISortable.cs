using System.Linq;

namespace TaskManager.Core
{
    public interface ISortable<T>
    {
        IQueryable<T> Sort(IQueryable<T> input);
    }
}