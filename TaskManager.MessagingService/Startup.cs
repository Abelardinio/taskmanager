using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ninject;
using TaskManager.Common.AspNetCore;
using TaskManager.Common.Data;
using TaskManager.Common.Data.AppSettings;
using TaskManager.Core.ConnectionContext;
using TaskManager.MessagingService.Data;
using TaskManager.MessagingService.DbConnection;
using TaskManager.MessagingService.WebApi.MessagingServices;

namespace TaskManager.MessagingService.WebApi
{
    public class Startup : StartupBase<AppSettingsModel, DependencyResolver>
    {
        private IApplicationBuilder _applicationBuilder;

        private IHubContext<T> GetHubContext<T>() where T : Hub
        {
            return _applicationBuilder.ApplicationServices.GetService<IHubContext<T>>();
        }

        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureServicesComponents(IServiceCollection services)
        {
            services.AddSignalR();
        }

        protected override void RegisterApplicationComponents(IApplicationBuilder app, IKernel kernel)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<TasksHub>("/tasks");
            });

            _applicationBuilder = app;

            kernel.Bind<IHubContext<TasksHub>>().ToMethod(x => GetHubContext<TasksHub>());
            kernel.Bind<IHostedService>().To<TasksMessagingService>();
            DependencyConfig.Configure(kernel);
            DependencyResolver.SetResolver(kernel);

            var tuple = kernel.Get<Tuple<IContextStorage, IConnectionContext>>();
            var storage = tuple.Item1;
            var context = tuple.Item2;

            using (context.Scope())
            {
                storage.Get().Database.Migrate();
            }
        }
    }
}
