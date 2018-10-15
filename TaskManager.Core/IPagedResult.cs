using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Core
{
    public interface IPagedResult<T>
    {
        IReadOnlyList<T> Items { get; }

        int PagesCount { get; }
    }
}