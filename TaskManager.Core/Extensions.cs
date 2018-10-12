using System.Linq;

namespace TaskManager.Core
{
    public static class Extensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> input, ISortable<T> filter)
        {
            return filter.Sort(input);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, IFilterable<T> filter)
        {
            return filter.Filter(input);
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> input, IFilter<T> filter)
        {
            return input.Sort(filter).Filter(filter);
        }
    }
}