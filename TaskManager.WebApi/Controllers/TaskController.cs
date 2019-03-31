using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Common.Data;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;
using TaskManager.WebApi.Model;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskDataProvider _taskDataProvider;
        private readonly IConnectionContext _context;

        public TaskController(ITaskDataProvider taskDataProvider, IConnectionContext context)
        {
            _taskDataProvider = taskDataProvider;
            _context = context;
        }

        [HttpGet]
        [Route("task")]
        public async Task<IPagedResult<ITask>> Get([FromQuery] TaskFilter filter)
        {
            using (_context.Scope())
            {
                return await _taskDataProvider.GetLiveTasks(filter.ProjectId).GetPagedResultAsync(filter);
            }
        }

        [HttpPost]
        [Route("task")]
        public async Task Add([FromBody] TaskInfoModel taskInfoModel)
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
                await _taskDataProvider.AddAsync(taskInfo);
            }
        }
        [HttpDelete]
        [Route("task/{taskId}")]
        public async Task Delete(int taskId)
        {
            using (_context.Scope())
            {
                await _taskDataProvider.UpdateStatusAsync(taskId, TaskStatus.Removed);
            }
        }

        [HttpPut]
        [Route("task/{taskId}/complete")]
        public async Task Put(int taskId)
        {
            using (_context.Scope())
            {
                await _taskDataProvider.UpdateStatusAsync(taskId, TaskStatus.Completed);
            }
        }
    }
}