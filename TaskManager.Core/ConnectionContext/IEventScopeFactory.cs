namespace TaskManager.Core.ConnectionContext
{
    public interface IEventScopeFactory
    {
        IEventConnectionScope Create();
    }
}