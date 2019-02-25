using TaskManager.Common.AspNetCore.AppSettings;

namespace TaskManager.Common.Data.AppSettings
{
    public class AppSettingsModel : AppSettingsModelBase
    {
        public DbConnectionSettings DbConnectionSettings { get; set; }
    }
}