using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DataAccessors
{
    public interface IPermissionsDataAccessor
    {
        Task UpdatePermissionsForUserAsync(int userId, IProjectPermission[] permissions);

        IQueryable<Permission> Get(int userId, int projectId);
    }
}