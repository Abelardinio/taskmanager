using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TaskManager.Common.AspNetCore;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataProviders;

namespace TaskManager.MessagingService.WebApi
{
    [Authorize]
    public class TasksHub : Hub
    {
        private readonly ITasksHubUsersDataProvider _usersDataProvider;
        private readonly IConnectionContext _connectionContext;

        public TasksHub(IDependencyResolver dependencyResolver)
        {
            var tuple = dependencyResolver.Resolve<Tuple<ITasksHubUsersDataProvider, IConnectionContext>>();
            _usersDataProvider = tuple.Item1;
            _connectionContext = tuple.Item2;
        }

        public override async Task OnConnectedAsync()
        {
            using (_connectionContext.Scope())
            {
                await _usersDataProvider.Add(Context.User.GetUserId(), Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
            _usersDataProvider.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(ex);
        }
    }
}