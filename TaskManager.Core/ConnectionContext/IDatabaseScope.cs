namespace TaskManager.Core.ConnectionContext
{
    public interface IDatabaseScope : ITransactionScope
    {
        bool IsDisposed { get; }
    }
}