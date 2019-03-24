using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Data;
using TaskManager.Core;
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
                return await _projectsDataProvider.Get().GetPagedResultAsync(filter);
            }
        }

        [HttpPost]
        [Route("projects")]
        public async Task Add([FromBody] ProjectInfoModel model)
        {
            using (_context.Scope())
            {
                await _projectsDataProvider.AddAsync(model);
            }
        }

        [HttpGet]
        [Route("projects/lookup")]
        public async Task<IEnumerable<ILookup>> Get()
        {
            using (_context.Scope())
            {
                return await _projectsDataProvider.Get().Select(x => new LookupModel {Id = x.Id, Name = x.Name})
                    .ToListAsync();
            }
        }
    }
}