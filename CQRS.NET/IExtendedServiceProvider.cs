using System;

namespace CQRS.NET {
    public interface IExtendedServiceProvider : IServiceProvider {
        TService? ResolveOptional<TService>() where TService : class;
        TService? ResolveOptional<TService>(object key) where TService : class;
        TService ResolveRequired<TService>() where TService : notnull;
        TService ResolveRequired<TService>(object key) where TService : notnull;
    }
}