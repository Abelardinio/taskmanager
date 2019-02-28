using System.Linq;
using System.Threading.Tasks;
using TaskManager.Common.DbConnection;
using TaskManager.Core;

namespace TaskManager.Common.Data
{
    public static class Extensions
    {
        public static Task<IPagedResult<T>> GetPagedResultAsync<T>(this IQueryable<T> input, IFilter<T> filter)
        {
            return DatabaseExtensions.GetPagedResultAsync(input, filter);
        }

        public static Task<IPagedResult<T>> GetPagedResultAsync<T>(this IQueryable<T> input, IUnsortableFilter<T> filter)
        {
            return DatabaseExtensions.GetPagedResultAsync(input, filter);
        }
    }
}