using Ninject;
using TaskManager.Core.DataAccessors;
using TaskManager.DbConnection.DataAccessors;

namespace TaskManager.Data
{
    public static class DependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<ITaskDataAccessor>().To<TaskDataAccessor>();
        }
    }
}