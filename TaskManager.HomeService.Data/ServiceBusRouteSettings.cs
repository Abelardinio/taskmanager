using TaskManager.Common.Data;
using TaskManager.ServiceBus;

namespace TaskManager.HomeService.Data
{
    public class ServiceBusRouteSettings : ServiceBusRouteSettingsBase
    {
        private const string AppName = "Home";
        public ServiceBusRouteSettings(IRoute[] routes, IExchange[] exchanges) : base(routes, exchanges)
        {
        }

        public override string ApplicationName => AppName;
    }
}