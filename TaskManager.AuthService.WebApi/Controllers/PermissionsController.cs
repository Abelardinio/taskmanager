using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.AuthService.WebApi.Models;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.WebApi.Controllers
{
    [Authorize(Roles = Roles.SiteAdministrator)]
    public class PermissionsController : Controller
    {
        private readonly IConnectionContext _context;
        private readonly IPermissionsDataProvider _permissionsDataProvider;

        public PermissionsController(IConnectionContext context, IPermissionsDataProvider permissionsDataProvider)
        {
            _context = context;
            _permissionsDataProvider = permissionsDataProvider;
        }

        [HttpGet]
        [Route("users/{userId}/permissions/{projectId}")]
        public async Task<Permission[]> Get(int userId, int projectId)
        {
            using (_context.Scope())
            {
                return await _permissionsDataProvider.Get(userId, projectId).ToArrayAsync();
            }
        }

        [HttpPut]
        [Route("users/{userId}/permissions")]
        public async Task Update(int userId, [FromBody] PermissionsModel model)
        {
            using (_context.Scope())
            {
                await _permissionsDataProvider.UpdatePermissionsForUserAsync(userId, model.Permissions);
            }
        }

        [HttpGet]
        [Route("users/permissions/lookup")]
        public ILookup[] Get()
        {
            return Permissions.Lookup;
        }
    }
}