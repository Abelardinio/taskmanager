using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface IUserTasksDataProvider
    {
        Task<IReadOnlyList<IUserTask>> Get(int userId);
    }
}