using Ninject;
using Ninject.Extensions.NamedScope;
using TaskManager.AuthService.Data.DataProviders;
using TaskManager.AuthService.DbConnection;
using TaskManager.AuthService.DbConnection.DataAccessors;
using TaskManager.Common.Data;
using TaskManager.Common.Data.AppSettings;
using TaskManager.Core;
using TaskManager.Core.ConnectionContext;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.EventAccessors;
using TaskManager.ServiceBus;
using TaskManager.ServiceBus.EventAccessors;

namespace TaskManager.AuthService.Data
{
    public static class DependencyConfig
    {
        public static void Register(IKernel kernel)
        {
            kernel.Bind<IConnectionScopeFactory, IContextStorage>().To<ContextFactory>().InCallScope();
            kernel.Bind<IConnectionContext>().To<ConnectionContext>();
            kernel.Bind<IConnectionSettings, IDbConnectionSettings, IAuthenticationSettings>().To<AppSettings>().InSingletonScope();
            kernel.Bind<IUsersDataAccessor>().To<UsersDataAccessor>();
            kernel.Bind<IUsersDataProvider>().To<UsersDataProvider>();
            kernel.Bind<ILoginDataProvider>().To<LoginDataProvider>();
            kernel.Bind<IHashCreator>().To<HashCreator>();
            kernel.Bind<ITokenProvider>().To<TokenProvider>();
            kernel.Bind<IPermissionsDataAccessor>().To<PermissionsDataAccessor>();
            kernel.Bind<IPermissionsDataProvider>().To<PermissionsDataProvider>();
            kernel.Bind<IPermissionsEventAccessor>().To<PermissionsEventAccessor>();
            kernel.Bind<IRouteSettings, IServiceBusClientSettings>().To<ServiceBusRouteSettings>().InSingletonScope();
        }
    }
}
