using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.DbConnection.Entities;

namespace TaskManager.DbConnection.DataAccessors
{
    public class TaskDataAccessor : ITaskDataAccessor
    {
        public async Task Add(ITaskInfo task)
        {
            using (var context = new Context())
            {
                context.Tasks.Add(new TaskEntity(task));
                await context.SaveChangesAsync();
            }
        }
    }
}