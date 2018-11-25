using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TaskManager.Common.AspNetCore;
using TaskManager.Data;
using TaskManager.Data.AppSettings;
using TaskManager.ServiceBus;

namespace TaskManager.WebApi
{
    public class Startup : StartupBase<AppSettingsModel, DependencyResolver>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureServicesComponents(IServiceCollection services)
        {
        }

        protected override void RegisterApplicationComponents(IApplicationBuilder app, IKernel kernel)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            DependencyConfig.Register(kernel);
            DependencyResolver.SetResolver(kernel);

            kernel.Get<IConnectionFactory>().Create();
        }
    }
}
