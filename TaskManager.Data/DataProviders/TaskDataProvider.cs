using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Common.Resources;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Exceptions;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Data.DataProviders
{
    public class TaskDataProvider : ITaskDataProvider
    {
        private readonly ITaskDataAccessor _taskDataAccessor;

        public TaskDataProvider(ITaskDataAccessor taskDataAccessor)
        {
            _taskDataAccessor = taskDataAccessor;
        }

        public Task Add(ITaskInfo task)
        {
            return _taskDataAccessor.Add(task);
        }

        public IQueryable<ITask> GetUnremoved()
        {
            return _taskDataAccessor.Get().Where(x => x.Status != TaskStatus.Removed && x.Status != TaskStatus.None);
        }

        public async Task UpdateStatusAsync(int taskId, TaskStatus status)
        {
            var task = await _taskDataAccessor.Get().FirstOrDefaultAsync(x => x.Id == taskId);

            if (task != null)
            {
                switch (status)
                {
                    case TaskStatus.Completed:
                        if (task.Status != TaskStatus.Active) throw new InvalidArgumentException(ErrorMessages.Tasks_CompleteUnactive);
                        await _taskDataAccessor.UpdateStatus(taskId, status);
                        return;
                    case TaskStatus.Removed:
                        if (task.Status != TaskStatus.Completed) throw new InvalidArgumentException(ErrorMessages.Tasks_RemoveUncompleted);
                        await _taskDataAccessor.UpdateStatus(taskId, status);
                        return;
                    default:
                        throw new InvalidArgumentException(ErrorMessages.Tasks_InvalidStatusParameterValue);
                }
            }

            throw new NotFoundException(String.Format(ErrorMessages.Tasks_NotFound, taskId));
        }
    }
}