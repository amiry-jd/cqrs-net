using Microsoft.Extensions.DependencyInjection;

namespace CQRS.NET.Autofac {
    
    public class DefaultExtendedServiceProvider :  IExtendedServiceProvider {

        private readonly IServiceProvider _scope;

        public DefaultExtendedServiceProvider(IServiceProvider scope)  {
            _scope = scope;
        }

        public TService? ResolveOptional<TService>() where TService : class
            => _scope.GetService<TService>();

        public TService? ResolveOptional<TService>(object key) where TService : class
            => _scope.GetKeyedService<TService>(key);

        public TService ResolveRequired<TService>() where TService : notnull
            => _scope.GetRequiredService<TService>();

        public TService ResolveRequired<TService>(object key) where TService : notnull
            => _scope.GetRequiredKeyedService<TService>(key);

        public object? GetService(Type serviceType) 
            => _scope.GetService(serviceType);

    }
}
