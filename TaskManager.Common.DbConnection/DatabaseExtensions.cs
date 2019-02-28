using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core;

namespace TaskManager.Common.DbConnection
{
    public static class DatabaseExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> input, ISortable<T> filter)
        {
            return filter.Sort(input);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> input, IFilterable<T> filter)
        {
            return filter.Filter(input);
        }

        public static Task<IPagedResult<T>> GetPagedResultAsync<T>(IQueryable<T> input, IFilter<T> filter)
        {
            return ExecuteAsync(input.Sort(filter).Filter(filter), filter);
        }

        public static  Task<IPagedResult<T>> GetPagedResultAsync<T>(IQueryable<T> input, IUnsortableFilter<T> filter)
        {
            return ExecuteAsync(input.Filter(filter), filter);
        }

        private static async Task<IPagedResult<T>> ExecuteAsync<T>(IQueryable<T> input, IPageable<T> filter)
        {
            return new PagedResult<T>
            {
                Items = await input.Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize)
                    .ToListAsync(),
                PagesCount = (await input.CountAsync() + filter.PageSize - 1) / filter.PageSize
            };
        }
    }
}