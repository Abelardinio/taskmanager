using Microsoft.Extensions.Options;
using TaskManager.Core;
using TaskManager.ServiceBus;

namespace TaskManager.Common.AspNetCore.AppSettings
{
    public abstract class AppSettingsBase : IConnectionSettings, IAuthenticationSettings
    {
        private readonly IConnectionSettings _connectionSettings;

        protected AppSettingsBase(IOptions<AppSettingsModelBase> options)
        {
            _connectionSettings = options.Value.RabbitMqConnection;
            SecretKey = options.Value.SecretKey;
        }

        public string UserName => _connectionSettings.UserName;

        public string Password => _connectionSettings.Password;

        public string VirtualHost => _connectionSettings.VirtualHost;

        public string HostName => _connectionSettings.HostName;

        public int Port => _connectionSettings.Port;

        public string SecretKey { get; set; }
    }
}