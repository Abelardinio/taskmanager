using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    public interface IHubClient<T> where T:Hub
    {
        void SendAsync(string method, object body);
    }
}