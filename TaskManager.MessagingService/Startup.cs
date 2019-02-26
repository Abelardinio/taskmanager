using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TaskManager.MessagingService.AppSettings;
using TaskManager.MessagingService.Dependency;
using TaskManager.Common.AspNetCore;
using TaskManager.ServiceBus;

namespace TaskManager.MessagingService
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

            DependencyConfig.Configure(kernel);
            DependencyResolver.SetResolver(kernel);

            kernel.Get<IConnectionFactory>().Create();
        }
    }
}
