using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.AspNetCore;
using TaskManager.Common.AspNetCore.Model;
using TaskManager.Common.Data;
using TaskManager.Core;
using TaskManager.Core.Enums;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;
using TaskManager.WebApi.Model;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectsDataProvider _projectsDataProvider;
        private readonly IConnectionContext _context;

        public ProjectsController(IConnectionContext connectionContext, IProjectsDataProvider projectsDataProvider)
        {
            _projectsDataProvider = projectsDataProvider;
            _context = connectionContext;
        }

        [HttpGet]
        [Route("projects")]
        public async Task<IPagedResult<IProject>> Get([FromQuery] ProjectsFilter filter)
        {
            using (_context.Scope())
            {
                return await _projectsDataProvider.Get(HttpContext.User.GetUserId()).GetPagedResultAsync(filter);
            }
        }

        [HttpGet]
        [Route("projects/{id}")]
        public async Task<IProject> Get(int id)
        {
            using (_context.Scope())
            {
                return await _projectsDataProvider.Get(HttpContext.User.GetUserId()).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.CreateProject)]
        [Route("projects")]
        public async Task Add([FromBody] ProjectInfoModel model)
        {
            using (_context.Scope())
            {
                await _projectsDataProvider.AddAsync(HttpContext.User.GetUserId(), model);
            }
        }

        [HttpGet]
        [Route("projects/lookup")]
        public async Task<IEnumerable<ILookup>> Get()
        {
            using (_context.Scope())
            {
                return await _projectsDataProvider.Get(HttpContext.User.GetUserId()).Select(x => new LookupModel {Id = x.Id, Name = x.Name})
                    .ToListAsync();
            }
        }

        [HttpGet]
        [Route("projects/lookup/addFeature")]
        public async Task<IEnumerable<ILookup>> GetLookupForAddProjectPage()
        {
            using (_context.Scope())
            {
                return await _projectsDataProvider.Get(HttpContext.User.GetUserId(), Permission.CreateFeature).Select(x => new LookupModel { Id = x.Id, Name = x.Name })
                    .ToListAsync();
            }
        }
    }
}