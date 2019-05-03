using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.AuthService.WebApi.Models;
using TaskManager.Common.AspNetCore;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;
using TaskManager.Common.Data;

namespace TaskManager.AuthService.WebApi.Controllers
{
    [Authorize(Roles = Roles.SiteAdministrator)]
    public class UsersController : Controller
    {
        private readonly IUsersDataProvider _usersDataProvider;
        private readonly IConnectionContext _context;

        public UsersController(IUsersDataProvider usersDataProvider, IConnectionContext context)
        {
            _usersDataProvider = usersDataProvider;
            _context = context;
        }

        [HttpPost]
        [Route("users")]
        public async Task Add([FromBody] UserInfoModel userModel)
        {
            using (_context.Scope())
            {
                await _usersDataProvider.AddAsync(userModel);
            }
        }

        [HttpGet]
        [Route("users")]
        public async Task<IPagedResult<UserModel>> Get([FromQuery] UsersFilter filter)
        {
            using (_context.Scope())
            {
                return await _usersDataProvider.Get().Where(x => x.Id != HttpContext.User.GetUserId())
                    .Select(x => new UserModel(x)).GetPagedResultAsync(filter);
            }
        }
    }
}