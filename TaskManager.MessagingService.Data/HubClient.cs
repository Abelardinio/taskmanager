using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService.Data
{
    public class HubClient<T> : IHubClient<T> where T: Hub
    {
        private readonly IHubContext<T> _hubContext;

        public HubClient(IHubContext<T> hubContext)
        {
            _hubContext = hubContext;
        }

        public void SendAsync(string method, object body, string[] connectionIds)
        {
            _hubContext.Clients.Clients(connectionIds).SendAsync(method, body);
        }
    }
}