namespace TaskManager.DbConnection
{
    /// <summary>
    /// Creates an instance of a new database context
    /// </summary>
    public interface IContextStorage
    {
        Context Get();
    }
}