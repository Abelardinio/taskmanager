using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Common.AspNetCore
{
    public static class AspNetCoreMvcExtensions
    {
        public static void AddCustomControllerActivation(this IServiceCollection services, Func<Type, object> activator)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            services.AddSingleton<IControllerActivator>(new DelegatingControllerActivator(
                context => activator(context.ActionDescriptor.ControllerTypeInfo.AsType())));
        }

        public static void AddCustomViewComponentActivation(this IServiceCollection services, Func<Type, object> activator)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            services.AddSingleton<IViewComponentActivator>(new DelegatingViewComponentActivator(activator));
        }

        public static void AddCustomTagHelperActivation(this IServiceCollection services, Func<Type, object> activator,
            Predicate<Type> applicationTypeSelector = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (activator == null) throw new ArgumentNullException(nameof(activator));

            // There are tag helpers OOTB in MVC. Letting the application container try to create them will fail
            // because of the dependencies these tag helpers have. This means that OOTB tag helpers need to remain
            // created by the framework's DefaultTagHelperActivator, hence the selector predicate.
            applicationTypeSelector =
                applicationTypeSelector ?? (type => !type.GetTypeInfo().Namespace.StartsWith("Microsoft"));

            services.AddSingleton<ITagHelperActivator>(provider =>
                new DelegatingTagHelperActivator(
                    customCreatorSelector: applicationTypeSelector,
                    customTagHelperCreator: activator,
                    defaultTagHelperActivator:
                        new DefaultTagHelperActivator(provider.GetRequiredService<ITypeActivatorCache>())));
        }

        public static Type[] GetControllerTypes(this IApplicationBuilder builder)
        {
            var manager = builder.ApplicationServices.GetRequiredService<ApplicationPartManager>();

            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);

            return feature.Controllers.Select(t => t.AsType()).ToArray();
        }

        public static Type[] GetViewComponentTypes(this IApplicationBuilder builder)
        {
            var manager = builder.ApplicationServices.GetRequiredService<ApplicationPartManager>();

            var feature = new ViewComponentFeature();
            manager.PopulateFeature(feature);

            return feature.ViewComponents.Select(t => t.AsType()).ToArray();
        }
    }

    public sealed class DelegatingControllerActivator : IControllerActivator
    {
        private readonly Func<ControllerContext, object> _controllerCreator;
        private readonly Action<ControllerContext, object> _controllerReleaser;

        public DelegatingControllerActivator(Func<ControllerContext, object> controllerCreator,
            Action<ControllerContext, object> controllerReleaser = null)
        {
            _controllerCreator = controllerCreator ?? throw new ArgumentNullException(nameof(controllerCreator));
            _controllerReleaser = controllerReleaser ?? ((_, __) => { });
        }

        public object Create(ControllerContext context) => _controllerCreator(context);
        public void Release(ControllerContext context, object controller) => _controllerReleaser(context, controller);
    }

    public sealed class DelegatingViewComponentActivator : IViewComponentActivator
    {
        private readonly Func<Type, object> _viewComponentCreator;
        private readonly Action<object> _viewComponentReleaser;

        public DelegatingViewComponentActivator(Func<Type, object> viewComponentCreator,
            Action<object> viewComponentReleaser = null)
        {
            _viewComponentCreator = viewComponentCreator ?? throw new ArgumentNullException(nameof(viewComponentCreator));
            _viewComponentReleaser = viewComponentReleaser ?? (_ => { });
        }

        public object Create(ViewComponentContext context) =>
            _viewComponentCreator(context.ViewComponentDescriptor.TypeInfo.AsType());

        public void Release(ViewComponentContext context, object viewComponent) =>
           _viewComponentReleaser(viewComponent);
    }

    internal sealed class DelegatingTagHelperActivator : ITagHelperActivator
    {
        private readonly Predicate<Type> _customCreatorSelector;
        private readonly Func<Type, object> _customTagHelperCreator;
        private readonly ITagHelperActivator _defaultTagHelperActivator;

        public DelegatingTagHelperActivator(Predicate<Type> customCreatorSelector, Func<Type, object> customTagHelperCreator,
            ITagHelperActivator defaultTagHelperActivator)
        {
            _customCreatorSelector = customCreatorSelector ?? throw new ArgumentNullException(nameof(customCreatorSelector));
            _customTagHelperCreator = customTagHelperCreator ?? throw new ArgumentNullException(nameof(customTagHelperCreator));
            _defaultTagHelperActivator = defaultTagHelperActivator ?? throw new ArgumentNullException(nameof(defaultTagHelperActivator));
        }

        public TTagHelper Create<TTagHelper>(ViewContext context) where TTagHelper : ITagHelper =>
            _customCreatorSelector(typeof(TTagHelper))
                ? (TTagHelper)_customTagHelperCreator(typeof(TTagHelper))
                : _defaultTagHelperActivator.Create<TTagHelper>(context);
    }
}