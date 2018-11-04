namespace TaskManager.Core.ConnectionContext
{
    public interface IConnectionContext
    {
        IConnectionScope Scope();

        ITransactionScope TransactionScope();

        IEventScope EventScope();
    }
}