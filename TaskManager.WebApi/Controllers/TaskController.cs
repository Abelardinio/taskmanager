using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Core;
using TaskManager.Core.DataProviders;
using TaskManager.Model;
using TaskManager.WebApi.Model;

namespace TaskManager.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskDataProvider _taskDataProvider;

        public TaskController(ITaskDataProvider taskDataProvider)
        {
            _taskDataProvider = taskDataProvider;
        }

        public async Task<IReadOnlyList<ITask>> Get()
        {
            return await _taskDataProvider.Get();
        }

        public async Task<ITask> Get(int id)
        {
            return await _taskDataProvider.Get(id);
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
            await _taskDataProvider.Add(taskInfo);
        }
    }
}