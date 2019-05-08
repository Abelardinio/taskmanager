using System;
using TaskManager.Core;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Messages;
using TaskManager.ServiceBus.Messages;

namespace TaskManager.ServiceBus.EventAccessors
{
    public class TaskEventAccessor : ITaskEventAccessor
    {
        private readonly IServiceBusClient _client;

        public TaskEventAccessor(IServiceBusClient client)
        {
            _client = client;
        }

        public void StatusUpdated(ITask task, IProject project)
        {
            _client.SendMessage(EventLookup.TaskStatusUpdated,
                new TaskStatusUpdatedMessage
                {
                    TaskId = task.Id,
                    AssignedUserId = task.AssignedUserId,
                    Status = task.Status,
                    ProjectId = project.Id,
                    CreatorId = project.CreatorId
                });
        }

        public void TaskAssigned(int userId, ITask task)
        {
            _client.SendMessage(EventLookup.TaskAssigned,
                new TaskAssignedMessage
                {
                    TaskId = task.Id,
                    AssignedUserId = userId,
                    Status = task.Status,
                    Added = task.Added,
                    Description = task.Description,
                    FeatureId = task.FeatureId,
                    Name = task.Name,
                    Priority = task.Priority,
                    TimeToComplete = task.TimeToComplete
                });
        }

        public void TaskUnassigned(int userId, int taskId)
        {
            _client.SendMessage(EventLookup.TaskUnassigned,
                new TaskUnassignedMessage
                {
                    TaskId = taskId,
                    UserId = userId
                });
        }

        public void OnStatusUpdated(Action<ITaskStatusUpdatedMessage> handler)
        {
           _client.Subscribe(EventLookup.TaskStatusUpdated, handler);
        }

        public void OnTaskAssigned(Action<ITaskAssignedMessage> handler)
        {
            _client.Subscribe(EventLookup.TaskAssigned, handler);
        }

        public void OnTaskUnassigned(Action<ITaskUnassignedMessage> handler)
        {
            _client.Subscribe(EventLookup.TaskUnassigned, handler);
        }
    }
}