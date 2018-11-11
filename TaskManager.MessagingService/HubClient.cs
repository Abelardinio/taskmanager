using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    public class HubClient<T> : IHubClient<T> where T: Hub
    {
        private readonly IHubContext<T> _hubContext;

        public HubClient(IHubContext<T> hubContext)
        {
            _hubContext = hubContext;
        }

        public void SendAsync(string method, object body)
        {
            _hubContext.Clients.All.SendAsync(method, body);
        }
    }
}