namespace TaskManager.ServiceBus
{
    public interface IRouteSettings
    {
        string ApplicationName { get; }
        IRoute[] Routes { get; }
        IExchange[] Exchanges { get; }
    }
}