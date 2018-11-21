using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Resources;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.Exceptions;
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

        public async Task AddAsync(ITaskInfo task)
        {
            var context = _contextStorage.Get();

            context.Tasks.Add(new TaskEntity(task));
            await context.SaveChangesAsync();
        }

        public IQueryable<ITask> Get()
        {
            return _contextStorage.Get().Tasks;
        }
        
        public async Task UpdateStatusAsync(int taskId, TaskStatus status)
        {
            var context = _contextStorage.Get();
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task != null)
            {
                switch (status)
                {
                    case TaskStatus.Completed:
                        if (task.Status != TaskStatus.Active) throw new InvalidArgumentException(ErrorMessages.Tasks_CompleteUnactive);
                        task.Status = status;
                        await context.SaveChangesAsync();
                        return;
                    case TaskStatus.Removed:
                        if (task.Status != TaskStatus.Completed) throw new InvalidArgumentException(ErrorMessages.Tasks_RemoveUncompleted);
                        task.Status = status;
                        await context.SaveChangesAsync();
                        return;
                    default:
                        throw new InvalidArgumentException(ErrorMessages.Tasks_InvalidStatusParameterValue);
                }
            }

            throw new NotFoundException(String.Format(ErrorMessages.Tasks_NotFound, taskId));
        }
    }
}