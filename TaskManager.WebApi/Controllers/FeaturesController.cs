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
using TaskManager.DbConnection.Models;
using TaskManager.WebApi.Model;

namespace TaskManager.WebApi.Controllers
{
    [Authorize]
    public class FeaturesController : Controller
    {
        private readonly IFeaturesDataProvider _featuresDataProvider;
        private readonly IConnectionContext _context;

        public FeaturesController(IFeaturesDataProvider featuresDataProvider, IConnectionContext context, IProjectsDataProvider projectsDataProvider)
        {
            _featuresDataProvider = featuresDataProvider;
            _context = context;
        }

        [HttpGet]
        [Route("features")]
        public async Task<IPagedResult<IFeatureModel>> Get([FromQuery] FeaturesFilter filter)
        {
            using (_context.Scope())
            {
                return await _featuresDataProvider.Get().GetPagedResultAsync(filter);
            }
        }

        [HttpGet]
        [Route("features/{id}")]
        public async Task<IFeature> Get(int id)
        {
            using (_context.Scope())
            {
                return await _featuresDataProvider.Get().FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        [HttpPost]
        [Route("features")]
        public async Task Add([FromBody] FeatureInfoModel model)
        {
            using (_context.Scope())
            {
                await _featuresDataProvider.AddAsync(model);
            }
        }


        [HttpGet]
        [Route("features/lookup")]
        public async Task<IEnumerable<ILookup>> Get([FromQuery] FeaturesLookupFilter filter)
        {
            using (_context.Scope())
            {
                return await _featuresDataProvider.Get()
                    .Where(x => !filter.ProjectId.HasValue || x.ProjectId == filter.ProjectId)
                    .Select(x => new LookupModel {Id = x.Id, Name = x.Name})
                    .ToListAsync();
            }
        }
    }
}