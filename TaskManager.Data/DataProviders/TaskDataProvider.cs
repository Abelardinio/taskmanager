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

        public IQueryable<ITask> Get(ITaskFilter filter)
        {
            var query = _taskDataAccessor.Get()
                .Where(x => x.Status != TaskStatus.Removed);

            switch (filter.Type)
            {
                case TakeType.None:
                    return query.OrderBy(x => x.Id).Take(filter.Count * 2);
                case TakeType.Before:
                    return query.Where(x => x.Id < filter.TaskId)
                        .OrderByDescending(x => x.Id)
                        .Take(filter.Count).OrderBy(x => x.Id);
                case TakeType.After:
                    return query.Where(x => x.Id > filter.TaskId)
                        .OrderBy(x => x.Id)
                        .Take(filter.Count);
                case TakeType.BeforeAndAfter:
                    return query.OrderBy(x => x.Id).Where(x => x.Id > filter.TaskId)
                        .Take(filter.Count)
                        .Concat(query
                            .Where(x => x.Id < filter.TaskId)
                            .OrderByDescending(x => x.Id)
                            .Take(filter.Count)).Concat(query.Where(x => x.Id == filter.TaskId)).OrderBy(x => x.Id);
                default:
                    throw new ArgumentOutOfRangeException(nameof(filter.Type));
            }

        }

        public Task<ITask> Get(int taskId)
        {
            return _taskDataAccessor.Get().FirstOrDefaultAsync(x => x.Id == taskId);
        }

        public async Task UpdateStatus(int taskId, TaskStatus status)
        {
            var task = await Get(taskId);

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
        }
    }
}