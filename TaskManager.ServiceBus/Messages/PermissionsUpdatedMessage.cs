using System;
using TaskManager.Core;

namespace TaskManager.ServiceBus.Messages
{
    [Serializable]
    public class PermissionsUpdatedMessage : IPermissionsUpdatedMessage
    {
        public int UserId { get; set; }
        public IProjectPermission[] Permissions { get; set; }
    }
}