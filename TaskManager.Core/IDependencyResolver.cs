namespace TaskManager.Core
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
    }
}