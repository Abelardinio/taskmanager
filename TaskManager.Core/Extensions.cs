using System.Linq;
using System.Threading.Tasks;

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

        public static Task<IPagedResult<T>> GetPagedResultAsync<T>(this IQueryable<T> input, IFilter<T> filter)
        {
            return Task.Run(() =>
            {
                var items = input.Sort(filter).Filter(filter);

                return (IPagedResult<T>) new PagedResult<T>
                {
                    Items = items.Skip(filter.PageSize * (filter.PageNumber - 1))
                        .Take(filter.PageSize)
                        .ToList(),
                    PagesCount = (items.Count() + filter.PageSize - 1) / filter.PageSize
                };
            });
        }
    }
}