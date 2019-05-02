using System;

namespace TaskManager.Core.EventAccessors
{
    public interface IPermissionsEventAccessor
    {
        void PermissionsUpdated(int userId, IProjectPermission[] permissions);

        void OnPermissionsUpdated(Action<IPermissionsUpdatedMessage> handler);
    }
}