using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Core;
using TaskManager.Core.DataProviders;
using TaskManager.Model;

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

        public async Task Post(TaskInfo taskInfo)
        {
            await _taskDataProvider.Add(taskInfo);
        }
    }
}