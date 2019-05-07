namespace TaskManager.Core
{
    public interface IMongoConnectionSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}