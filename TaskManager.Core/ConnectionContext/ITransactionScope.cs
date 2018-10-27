namespace TaskManager.Core.ConnectionContext
{
    public interface ITransactionScope : IConnectionScope
    {
        void Commit();

        void Rollback();
    }
}