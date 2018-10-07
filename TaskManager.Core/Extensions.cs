using System.Linq;

namespace TaskManager.Core
{
    public static class Extensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> input, ISortable<T> filter)
        {
            return filter.Sort(input);
        }
    }
}