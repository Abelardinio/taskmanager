namespace TaskManager.Core.ConnectionContext
{
    public interface IConnectionScopeFactory
    {
        IDatabaseScope Create(bool isInTransactionScope);
    }
}