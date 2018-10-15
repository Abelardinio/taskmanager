using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Core
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public int PagesCount { get; set; }
    }
}