using Microsoft.Extensions.Options;
using TaskManager.Common.AspNetCore.AppSettings;
using TaskManager.Core;

namespace TaskManager.Data.AppSettings
{
    public class AppSettings : AppSettingsBase, IDbConnectionSettings
    {
        private readonly IDbConnectionSettings _dbConnectionSettings;
        public AppSettings(IOptions<AppSettingsModel> options) : base(options)
        {
            _dbConnectionSettings = options.Value.DbConnectionSettings;
        }

        public string ConnectionString
        {
            get { return _dbConnectionSettings.ConnectionString; }
        }
    }
}