using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskStatus = TaskManager.Core.TaskStatus;

namespace TaskManager.Data.DataProviders
{
    public class TaskDataProvider : ITaskDataProvider
    {
        private readonly ITaskDataAccessor _taskDataAccessor;
        private readonly ITaskEventAccessor _taskEventAccessor;
        private readonly IConnectionContext _connectionContext;
        private readonly IFeaturesDataAccessor _featuresDataAccessor;

        public TaskDataProvider(
            ITaskDataAccessor taskDataAccessor,
            ITaskEventAccessor taskEventAccessor,
            IConnectionContext connectionContext,
            IFeaturesDataAccessor featuresDataAccessor)
        {
            _taskDataAccessor = taskDataAccessor;
            _taskEventAccessor = taskEventAccessor;
            _connectionContext = connectionContext;
            _featuresDataAccessor = featuresDataAccessor;
        }

        public Task AddAsync(ITaskInfo task)
        {
            return _taskDataAccessor.AddAsync(task);
        }

        public IQueryable<ITask> GetLiveTasks(int? projectId)
        {
            var result = _taskDataAccessor.Get()
                .Where(x => x.Status != TaskStatus.Removed && x.Status != TaskStatus.None);

            return projectId.HasValue ? result.Where(x=> _featuresDataAccessor.Get().FirstOrDefault(y=>y.Id == x.FeatureId).ProjectId == projectId.Value) : result;
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