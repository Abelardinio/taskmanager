using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TaskManager.MessagingService.AppSettings;
using TaskManager.MessagingService.Dependency;
using TaskManager.Common.AspNetCore;

namespace TaskManager.MessagingService
{
    public class Startup : StartupBase<AppSettingsModel, DependencyResolver>
    {
        private IHubContext<TasksHub> _hubContext;
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureServicesComponents(IServiceCollection services)
        {
            services.AddSignalR();
            _hubContext = services.BuildServiceProvider().GetRequiredService<IHubContext<TasksHub>>();
        }

        protected override void RegisterApplicationComponents(IApplicationBuilder app, IKernel kernel)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<TasksHub>("/tasks");
            });


            kernel.Bind<IHubContext<TasksHub>>().ToConstant(_hubContext);

            DependencyConfig.Configure(kernel);
            DependencyResolver.SetResolver(kernel);
        }
    }
}
