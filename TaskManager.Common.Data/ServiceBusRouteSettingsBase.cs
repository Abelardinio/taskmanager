using TaskManager.ServiceBus;

namespace TaskManager.Common.Data
{
    public abstract class ServiceBusRouteSettingsBase : IRouteSettings, IServiceBusClientSettings
    {
        protected ServiceBusRouteSettingsBase(IRoute[] routes, IExchange[] exchanges)
        {
            Routes = routes;
            Exchanges = exchanges;
        }
        public abstract string ApplicationName { get; }
        public IRoute[] Routes { get; }
        public IExchange[] Exchanges { get; }
    }
}