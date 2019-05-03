using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.EventAccessors;

namespace TaskManager.Common.Data.HostedServices
{
    public class PermissionsHostedService : IHostedService
    {
        private readonly IConnectionContext _connectionContext;
        private readonly IPermissionsEventAccessor _permissionsEventAccessor;
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;
        private IEventScope _eventScope;

        public PermissionsHostedService(
            IConnectionContext connectionContext,
            IPermissionsEventAccessor permissionsEventAccessor,
            IPermissionsDataAccessor permissionsDataAccessor)
        {
            _connectionContext = connectionContext;
            _permissionsEventAccessor = permissionsEventAccessor;
            _permissionsDataAccessor = permissionsDataAccessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventScope = _connectionContext.EventScope();
            _permissionsEventAccessor.OnPermissionsUpdated(OnPermissionsUpdated);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventScope?.Dispose();
            return Task.CompletedTask;
        }

        private void OnPermissionsUpdated(IPermissionsUpdatedMessage message)
        {
            using (_connectionContext.Scope())
            {
                _permissionsDataAccessor.UpdatePermissionsForUserAsync(message.UserId, message.Permissions).Wait();
            }
        }
    }
}