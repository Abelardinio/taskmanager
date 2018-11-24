using System.Collections.Generic;

namespace TaskManager.Core
{
    public interface IPagedResult<T>
    {
        IReadOnlyList<T> Items { get; }

        int PagesCount { get; }
    }
}