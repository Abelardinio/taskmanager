namespace TaskManager.ServiceBus
{
    public interface IConnectionSettings
    {
        string UserName { get; }
        string Password { get; }
        string VirtualHost { get; }
        string HostName {get; }
        int Port { get; }
    }
}