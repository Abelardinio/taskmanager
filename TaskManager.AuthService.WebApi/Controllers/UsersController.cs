using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.AuthService.WebApi.Models;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;

namespace TaskManager.AuthService.WebApi.Controllers
{
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
        public async Task Add([FromBody] UserModel userModel)
        {
            using (_context.Scope())
            {
                await _usersDataProvider.AddAsync(userModel);
            }
        }
    }
}