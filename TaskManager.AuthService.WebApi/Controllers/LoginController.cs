using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.AuthService.WebApi.Models;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;

namespace TaskManager.AuthService.WebApi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILoginDataProvider _loginDataProvider;
        private readonly IConnectionContext _context;

        public LoginController(ILoginDataProvider loginDataProvider, IConnectionContext context)
        {
            _loginDataProvider = loginDataProvider;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<object> Authenticate([FromBody] LoginModel model)
        {
            using (_context.Scope())
            {
                return new
                {
                    Token = await _loginDataProvider.AuthenticateAsync(model.Username, model.Password)
                };
            }
        }
    }
}