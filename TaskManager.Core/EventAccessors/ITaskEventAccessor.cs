using System;
using TaskManager.Core.Messages;

namespace TaskManager.Core.EventAccessors
{
    public interface ITaskEventAccessor
    {
        void StatusUpdated(ITask task, IProject project);

        void TaskAssigned(int userId, ITask task);

        void TaskUnassigned(int userId, int taskId);

        void OnStatusUpdated(Action<ITaskStatusUpdatedMessage> handler);

        void OnTaskAssigned(Action<ITaskAssignedMessage> handler);

        void OnTaskUnassigned(Action<ITaskUnassignedMessage> handler);
    }
}