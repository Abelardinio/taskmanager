using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using TaskManager.AuthService.Data;
using TaskManager.AuthService.DbConnection;
using TaskManager.Common.AspNetCore;
using TaskManager.Common.Data.AppSettings;
using TaskManager.Core.ConnectionContext;
using TaskManager.ServiceBus;

namespace TaskManager.AuthService.WebApi
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
