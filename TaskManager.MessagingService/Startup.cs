using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using TaskManager.Common.AspNetCore;
using TaskManager.Core;
using TaskManager.MessagingService.AppSettings;
using TaskManager.MessagingService.Dependency;

namespace TaskManager.MessagingService
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> _scopeProvider = new AsyncLocal<Scope>();
        private IOptions<AppSettings.AppSettingsModel> _settings;
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);
        private object RequestScope(IContext context) => _scopeProvider.Value;

        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
        }

        public IConfiguration AppConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();


            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.Configure<AppSettings.AppSettingsModel>(AppConfiguration);

            _settings = services.BuildServiceProvider().GetService<IOptions<AppSettings.AppSettingsModel>>();

            services.AddSignalR();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHostedService<MessagingHostedService>();
            services.AddSingleton<IDependencyResolver, DependencyResolver>();

            services.AddRequestScopingMiddleware(() => _scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Kernel = RegisterApplicationComponents(app, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<TasksHub>("/tasks");
            });
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // IKernelConfiguration config = new KernelConfiguration();
            Kernel = new StandardKernel();

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                Kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }

            // Cross-wire required framework services
            Kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);
            Kernel.Bind<ILoggerFactory>().ToConstant(loggerFactory);
            Kernel.Bind<IOptions<AppSettingsModel>>().ToMethod(context => _settings).InSingletonScope();

            DependencyConfig.Configure(Kernel);
            DependencyResolver.SetResolver(Kernel);

            return Kernel;
        }

        private sealed class Scope : DisposableObject { }
    }

    public static class BindingHelpers
    {
        public static void BindToMethod<T>(this IKernel config, Func<T> method) => config.Bind<T>().ToMethod(c => method());
    }
}
