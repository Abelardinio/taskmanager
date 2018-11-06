namespace TaskManager.Core.ConnectionContext
{
    public interface IConnectionContext : IEventConnectionContext
    {
        IConnectionScope Scope();

        ITransactionScope TransactionScope();
    }
}