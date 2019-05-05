using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService.Data
{
    public interface IHubClient<T> where T:Hub
    {
        void SendAsync(string method, object body, string[] connectionIds);
    }
}