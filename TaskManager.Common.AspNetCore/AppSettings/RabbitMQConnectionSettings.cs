using TaskManager.ServiceBus;

namespace TaskManager.Common.AspNetCore.AppSettings
{
    public class RabbitMqConnectionSettings : IConnectionSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
    }
}