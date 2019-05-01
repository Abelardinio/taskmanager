using TaskManager.Common.Data;
using TaskManager.ServiceBus;

namespace TaskManager.Data
{
    public class ServiceBusRouteSettings : ServiceBusRouteSettingsBase
    {
        private const string AppName = "Api";
        public ServiceBusRouteSettings(IRoute[] routes, IExchange[] exchanges) : base(routes, exchanges)
        {
        }

        public override string ApplicationName => AppName;
    }
}