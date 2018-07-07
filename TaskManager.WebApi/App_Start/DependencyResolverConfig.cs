using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using Ninject;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.Filter;
using TaskManager.Core.DataProviders;
using TaskManager.Data.DataProviders;

namespace TaskManager.WebApi
{
    public static class DependencyResolverConfig
    {
        public static void Configure()
        {
            var kernel = new StandardKernel();
            try
            {
                Data.DependencyConfig.Register(kernel);

                kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));
                kernel.Bind<DefaultFilterProviders>()
                    .ToConstant(new DefaultFilterProviders(new[] {new NinjectFilterProvider(kernel)}.AsEnumerable()));
                Register(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void Register(IKernel kernel)
        {
            kernel.Bind<ITaskDataProvider>().To<TaskDataProvider>();
        }
    }
}