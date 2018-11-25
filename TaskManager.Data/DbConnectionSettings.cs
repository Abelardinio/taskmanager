using TaskManager.Core;

namespace TaskManager.Data
{
    public class DbConnectionSettings : IDbConnectionSettings
    {
        public string ConnectionString { get; set; }
    }
}