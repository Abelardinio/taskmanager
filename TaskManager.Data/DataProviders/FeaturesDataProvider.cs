using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Resources;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.DbConnection.Models;

namespace TaskManager.Data.DataProviders
{
    public class FeaturesDataProvider : IFeaturesDataProvider
    {
        private readonly IFeaturesDataAccessor _featuresDataAccessor;
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;
        private readonly IProjectsDataAccessor _projectsDataAccessor;

        public FeaturesDataProvider(
            IFeaturesDataAccessor featuresDataAccessor,
            IPermissionsDataAccessor permissionsDataAccessor,
            IProjectsDataAccessor projectsDataAccessor)
        {
            _featuresDataAccessor = featuresDataAccessor;
            _permissionsDataAccessor = permissionsDataAccessor;
            _projectsDataAccessor = projectsDataAccessor;
        }

        public async Task AddAsync(int userId, IFeatureInfo info)
        {
            if (await _projectsDataAccessor.Get().AnyAsync(x=>x.Id ==info.ProjectId && x.CreatorId == userId) ||
                await _permissionsDataAccessor.HasPermission(userId, info.ProjectId, Permission.CreateFeature))
            {
                await _featuresDataAccessor.AddAsync(info);
                return;
            }

            throw new NoPermissionsForOperationException(ErrorMessages.NoPermissionsForOperation);
        }

        public IQueryable<IFeatureModel> Get(int userId)
        {
            return _featuresDataAccessor.Get(userId);
        }
    }
}