using TaskManager.Core;

namespace TaskManager.Common.Data
{
    public class DbConnectionSettings : IDbConnectionSettings
    {
        public string ConnectionString { get; set; }
    }
}