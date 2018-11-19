using System;
using Ninject;
using TaskManager.Core;

namespace TaskManager.MessagingService.Dependency
{
    public class DependencyResolver : IDependencyResolver
    {
        private static IKernel _kernel;
        public static void SetResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
        public T Resolve<T>()
        {
           return _kernel.Get<T>();
        }

        public object Resolve(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
