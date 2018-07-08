using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IReadOnlyList<ITask>> Get()
        {
            using (var context = new Context())
            {
                return await context.Tasks.ToListAsync();
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