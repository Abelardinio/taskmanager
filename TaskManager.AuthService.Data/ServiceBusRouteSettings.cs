using TaskManager.Common.Data;
using TaskManager.ServiceBus;

namespace TaskManager.AuthService.Data
{
    public class ServiceBusRouteSettings : ServiceBusRouteSettingsBase
    {
        private const string AppName = "Auth";
        public ServiceBusRouteSettings(IRoute[] routes, IExchange[] exchanges) : base(routes, exchanges)
        {
        }

        public override string ApplicationName => AppName;
    }
}