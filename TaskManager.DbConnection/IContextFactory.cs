namespace TaskManager.DbConnection
{
    public interface IContextFactory
    {
        Context Get();
    }
}