namespace TaskManager.Core.ConnectionContext
{
    public interface IEventConnectionScope : IEventScope
    {
        bool IsDisposed { get; }
    }
}