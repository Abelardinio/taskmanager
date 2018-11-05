using Microsoft.Extensions.Options;
using TaskManager.ServiceBus;

namespace TaskManager.MessagingService.AppSettings
{
    public class AppSettings : IConnectionSettings
    {
        private readonly IConnectionSettings _connectionSettings;

        public AppSettings(IOptions<AppSettingsModel> options)
        {
            _connectionSettings = options.Value.RabbitMqConnection;
        }

        public string UserName
        {
            get { return _connectionSettings.UserName; }
        }
        public string Password
        {
            get { return _connectionSettings.Password; }
        }
        public string VirtualHost
        {
            get { return _connectionSettings.VirtualHost; }
        }
        public string HostName
        {
            get { return _connectionSettings.HostName; }
        }
        public int Port
        {
            get { return _connectionSettings.Port; }
        }
    }
}