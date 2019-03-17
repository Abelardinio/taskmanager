using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaskManager.MessagingService
{
    [Authorize]
    public class TasksHub : Hub
    {
    }
}