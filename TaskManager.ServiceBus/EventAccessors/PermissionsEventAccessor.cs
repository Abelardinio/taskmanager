using System;
using TaskManager.Core;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.ServiceBus.Messages;

namespace TaskManager.ServiceBus.EventAccessors
{
    public class PermissionsEventAccessor : IPermissionsEventAccessor
    {
        private readonly IServiceBusClient _client;

        public PermissionsEventAccessor(IServiceBusClient client)
        {
            _client = client;
        }

        public void PermissionsUpdated(int userId, IProjectPermission[] permissions)
        {
            _client.SendMessage(EventLookup.PermissionsUpdated,
                new PermissionsUpdatedMessage {UserId = userId, Permissions = permissions});
        }

        public void OnPermissionsUpdated(Action<IPermissionsUpdatedMessage> handler)
        {
            _client.Subscribe(EventLookup.PermissionsUpdated, handler);
        }
    }
}