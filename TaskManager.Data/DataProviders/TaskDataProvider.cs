using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Resources;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Enums;
using TaskManager.Core.EventAccessors;
using TaskManager.Core.Exceptions;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Data.DataProviders
{
    public class TaskDataProvider : ITaskDataProvider
    {
        private readonly ITaskDataAccessor _taskDataAccessor;
        private readonly ITaskEventAccessor _taskEventAccessor;
        private readonly IConnectionContext _connectionContext;
        private readonly IPermissionsDataAccessor _permissionsDataAccessor;
        private readonly IProjectsDataAccessor _projectsDataAccessor;

        public TaskDataProvider(
            ITaskDataAccessor taskDataAccessor,
            ITaskEventAccessor taskEventAccessor,
            IConnectionContext connectionContext,
            IPermissionsDataAccessor permissionsDataAccessor, IProjectsDataAccessor projectsDataAccessor)
        {
            _taskDataAccessor = taskDataAccessor;
            _taskEventAccessor = taskEventAccessor;
            _connectionContext = connectionContext;
            _permissionsDataAccessor = permissionsDataAccessor;
            _projectsDataAccessor = projectsDataAccessor;
        }

        public async Task AddAsync(int userId, ITaskInfo task)
        {
            if (await _projectsDataAccessor.IsProjectCreator(userId, task.FeatureId.Value) ||
                await _permissionsDataAccessor.HasPermissionForFeature(userId, task.FeatureId.Value, Permission.CreateTask))
            {
                await _taskDataAccessor.AddAsync(task);
            }
            else
            {
                throw new NoPermissionsForOperationException(ErrorMessages.NoPermissionsForOperation);
            }
        }

        public IQueryable<ITask> GetLiveTasks(int userId, int? projectId)
        {
            return _taskDataAccessor.Get(userId, projectId)
                .Where(x => x.Status != TaskStatus.Removed && x.Status != TaskStatus.None);
        }

        public async Task UpdateStatusAsync(int taskId, TaskStatus status)
        {
            await _taskDataAccessor.UpdateStatusAsync(taskId, status);

            using (_connectionContext.EventScope())
            {
                _taskEventAccessor.StatusUpdated(taskId, status);
            }
        }
    }
}