using TaskManager.Core;

namespace TaskManager.Common.AspNetCore.AppSettings
{
    public  class AppSettingsModelBase : IAuthenticationSettings
    {
        public RabbitMqConnectionSettings RabbitMqConnection { get; set; }
        public string SecretKey { get; set; }
    }
}