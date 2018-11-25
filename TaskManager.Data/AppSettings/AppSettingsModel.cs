using TaskManager.Common.AspNetCore.AppSettings;

namespace TaskManager.Data.AppSettings
{
    public class AppSettingsModel : AppSettingsModelBase
    {
        public DbConnectionSettings DbConnectionSettings { get; set; }
    }
}