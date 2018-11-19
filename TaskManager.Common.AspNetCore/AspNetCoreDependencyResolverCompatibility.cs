using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManager.Core;

namespace TaskManager.Common.AspNetCore
{
    public static class AspNetCoreDependencyResolverCompatibility
    {
        public static void AddDependencyResolverCompatibility(this IServiceCollection services)
        {
            services.AddSingleton<IControllerActivator, CustomControllerActivator>();
            services.AddSingleton<HostedServiceExecutor, CustomHostedServiceExecutor>();
        }

        public static Type[] GetControllerTypes(this IApplicationBuilder builder)
        {
            var manager = builder.ApplicationServices.GetRequiredService<ApplicationPartManager>();

            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);

            return feature.Controllers.Select(t => t.AsType()).ToArray();
        }
    }

    public sealed class CustomControllerActivator : DefaultControllerActivator
    {
        private readonly IDependencyResolver _resolver;
        public CustomControllerActivator(ITypeActivatorCache typeActivatorCache, IDependencyResolver resolver) : base(typeActivatorCache)
        {
            _resolver = resolver;
        }

        public override object Create(ControllerContext context) =>
            _resolver.Resolve(context.ActionDescriptor.ControllerTypeInfo.AsType());
    }

    public sealed class CustomHostedServiceExecutor : HostedServiceExecutor
    {
        public CustomHostedServiceExecutor(ILogger<HostedServiceExecutor> logger, IDependencyResolver resolver) 
            : base(logger, resolver.Resolve<IEnumerable<IHostedService>>())
        {
        }
    }
}