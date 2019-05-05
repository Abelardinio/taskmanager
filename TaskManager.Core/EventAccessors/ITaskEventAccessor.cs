using System;
using TaskManager.Core.Messages;

namespace TaskManager.Core.EventAccessors
{
    public interface ITaskEventAccessor
    {
        void StatusUpdated(int taskId, TaskStatus status, IProject project);

        void OnStatusUpdated(Action<ITaskStatusUpdatedMessage> handler);
    }
}