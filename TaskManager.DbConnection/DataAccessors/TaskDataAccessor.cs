using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.DbConnection.Entities;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.DbConnection.DataAccessors
{
    public class TaskDataAccessor : ITaskDataAccessor
    {
        private readonly IContextStorage _contextStorage;

        public TaskDataAccessor(IContextStorage contextStorage)
        {
            _contextStorage = contextStorage;
        }

        public async Task Add(ITaskInfo task)
        {
            var context = _contextStorage.Get();

            context.Tasks.Add(new TaskEntity(task));
            await context.SaveChangesAsync();
        }

        public IQueryable<ITask> Get()
        {
            return _contextStorage.Get().Tasks;
        }

        public async Task UpdateStatus(int taskId, TaskStatus status)
        {
            var context = _contextStorage.Get();
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new ArgumentException($"Task with taskId = '{taskId}' not found ", nameof(taskId));
            }
            task.Status = status;
            await context.SaveChangesAsync();
        }
    }
}