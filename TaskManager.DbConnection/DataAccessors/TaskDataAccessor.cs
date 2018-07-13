using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public async Task<IReadOnlyList<ITask>> Get(ITaskFilter filter)
        {
            using (var context = new Context())
            {
                return await context.Tasks.OrderBy(x=>x.Id).Skip(() => filter.Skip).Take(() => filter.Take).ToListAsync();
            }
        }

        public async Task<ITask> Get(int taskId)
        {
            using (var context = new Context())
            {
                return await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            }
        }
    }
}