using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Ninject;
using TaskManager.Core;
using TaskManager.ServiceBus;

namespace TaskManager.Common.AspNetCore
{
    public abstract class StartupBase<TSettingsModel, TResolver> 
        where TSettingsModel : class, IAuthenticationSettings, new()
        where TResolver : class, IDependencyResolver
    {
        private readonly IConfiguration _configuration;
        private IOptions<TSettingsModel> _settings;
        private IKernel _kernel;

        protected StartupBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                config => {
                    config.Filters.Add(typeof(ApiExceptionFilter));
                }
            ).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); 

            services.AddOptions();

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.Configure<TSettingsModel>(_configuration);

            _settings = services.BuildServiceProvider().GetService<IOptions<TSettingsModel>>();

            var key = Encoding.ASCII.GetBytes(_settings.Value.SecretKey);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDependencyResolverCompatibility();
            services.AddSingleton<IDependencyResolver, TResolver>();

            ConfigureServicesComponents(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            app.UseAuthentication();

            _kernel = new StandardKernel();

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                _kernel.Bind(ctrlType).ToSelf();
            }

            _kernel.Bind<ILoggerFactory>().ToConstant(loggerFactory);
            _kernel.Bind<IOptions<TSettingsModel>>().ToConstant(_settings);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            lifetime.ApplicationStopped.Register(() => OnShutdown(_kernel));

            RegisterApplicationComponents(app, _kernel);
        }

        protected void OnShutdown(IKernel kernel)
        {
            kernel.Get<IConnectionStorage>().Get().Dispose();
        }

        protected abstract void ConfigureServicesComponents(IServiceCollection services);

        protected abstract void RegisterApplicationComponents(IApplicationBuilder app, IKernel kernel);
    }
}