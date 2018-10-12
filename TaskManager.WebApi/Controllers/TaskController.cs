using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Core;
using TaskManager.Core.DataProviders;
using TaskManager.Model;
using TaskManager.WebApi.Model;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskDataProvider _taskDataProvider;
        private readonly IConnectionContext _context;

        public TaskController(ITaskDataProvider taskDataProvider, IConnectionContext context)
        {
            _taskDataProvider = taskDataProvider;
            _context = context;
        }

        public async Task<IReadOnlyList<ITask>> Get([FromUri] TaskFilter filter)
        {
            using (_context.Scope())
            {
                return await _taskDataProvider.GetUnremoved().ApplyFilter(filter).ToListAsync();
            }
        }

        public async Task<ITask> Get(int id)
        {
            using (_context.Scope())
            {
                return await _taskDataProvider.Get(id);
            }
        }

        public async Task Add(TaskInfoModel taskInfoModel)
        {
            var taskInfo = new TaskInfo
            {
                Name = taskInfoModel.Name,
                Description = taskInfoModel.Description,
                Priority = taskInfoModel.Priority,
                Added = DateTime.Now,
                TimeToComplete = DateTime.Now
                    .AddDays(taskInfoModel.TimeToComplete.Days + taskInfoModel.TimeToComplete.Weeks * 7)
                    .AddHours(taskInfoModel.TimeToComplete.Hours)
            };

            using (_context.Scope())
            {
                await _taskDataProvider.Add(taskInfo);
            }
        }

        public async Task Delete(int id)
        {
            using (_context.Scope())
            {
                await _taskDataProvider.UpdateStatus(id, TaskStatus.Removed);
            }
        }

        [Route("task/{taskId}/complete")]
        public async Task Put(int taskId)
        {
            using (_context.Scope())
            {
                await _taskDataProvider.UpdateStatus(taskId, TaskStatus.Completed);
            }
        }
    }
}