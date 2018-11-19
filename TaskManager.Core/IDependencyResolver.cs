using System;

namespace TaskManager.Core
{
    public interface IDependencyResolver
    {
        T Resolve<T>();

        object Resolve(Type type);
    }
}