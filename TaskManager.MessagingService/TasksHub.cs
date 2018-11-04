using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    public class TasksHub : Hub
    {
        public Task BroadcastHello()
        {
            return Clients.All.SendAsync("/tasks");
        }
    }
}