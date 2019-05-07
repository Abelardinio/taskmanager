using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Common.AspNetCore;
using TaskManager.Core;
using TaskManager.Core.DataProviders;

namespace TaskManager.HomeService.WebApi.Controllers
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private readonly IUserTasksDataProvider _userTasksDataProvider;

        public UserTasksController(IUserTasksDataProvider userTasksDataProvider)
        {
            _userTasksDataProvider = userTasksDataProvider;
        }

        [HttpGet]
        [Route("usertasks")]
        public async Task<IReadOnlyList<IUserTask>> Get()
        {
            return await _userTasksDataProvider.Get(HttpContext.User.GetUserId());
        }
    }
}
