using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.EventAccessors;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;

namespace TaskManager.MessagingService
{
    public static class DependencyConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IConnectionSettings, AppSettings.AppSettings>();
        }
    }
}