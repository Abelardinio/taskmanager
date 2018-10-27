namespace TaskManager.DbConnection
{
    /// <summary>
    /// Creates an istance of a new database context
    /// </summary>
    public interface IContextStorage
    {
        Context Get();
    }
}