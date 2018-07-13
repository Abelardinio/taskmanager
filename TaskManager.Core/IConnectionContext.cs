namespace TaskManager.Core
{
    public interface IConnectionContext
    {
        IConnectionScope Scope();
    }
}