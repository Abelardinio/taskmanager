using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskManager.Core;
using TaskManager.Core.DataAccessors;
using TaskManager.HomeService.DbConnection.Entities;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.HomeService.DbConnection.DataAccessors
{
    public class UserTasksDataAccessor : IUserTasksDataAccessor
    {
        private readonly IContext _context;

        public UserTasksDataAccessor(IContext context)
        {
            _context = context;
        }

        public Task AddTaskToUser(int userId, int taskId, TaskStatus status, ITaskInfo taskInfo)
        {
            return _context.Users.UpdateOneAsync(x => x.UserId == userId,
                Builders<UserEntity>.Update.Push(x => x.Tasks, new TaskEntity
                {
                    Id = taskId,
                    Added = taskInfo.Added,
                    TimeToComplete = taskInfo.TimeToComplete,
                    FeatureId = taskInfo.FeatureId,
                    Name = taskInfo.Name,
                    Description = taskInfo.Description,
                    Priority = taskInfo.Priority,
                    Status = status
                }), new UpdateOptions {IsUpsert = true});
        }

        public Task RemoveTask(int userId, int taskId)
        {
            return _context.Users.UpdateOneAsync(x => x.UserId == userId,
                Builders<UserEntity>.Update.PullFilter(x => x.Tasks, x => x.Id == taskId));
        }

        public Task ChangeTaskStatus(int userId, int taskId, TaskStatus status)
        {
            return _context.Users.UpdateOneAsync(x => x.UserId == userId && x.Tasks.Any(t=>t.Id == taskId),
                Builders<UserEntity>.Update.Set("Tasks.$.Status", status));
        }
    }
}