using TaskManager.Common.Data;
using TaskManager.ServiceBus;

namespace TaskManager.MessagingService.Data
{
    public class ServiceBusRouteSettings : ServiceBusRouteSettingsBase
    {
        private const string AppName = "Messaging";
        public ServiceBusRouteSettings(IRoute[] routes, IExchange[] exchanges) : base(routes, exchanges)
        {
        }

        public override string ApplicationName => AppName;
    }
}