using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace CQRS.NET.Autofac {
    public class AutofacExtendedServiceProvider : AutofacServiceProvider, IExtendedServiceProvider {

        private readonly ILifetimeScope _scope;

        public AutofacExtendedServiceProvider(ILifetimeScope scope) : base(scope) {
            _scope = scope;
        }

        public TService? ResolveOptional<TService>() where TService : class => _scope.ResolveOptional<TService>();

        public TService? ResolveOptional<TService>(object key) where TService : class => _scope.ResolveOptionalKeyed<TService>(key);

        public TService ResolveRequired<TService>() where TService : notnull => _scope.Resolve<TService>();

        public TService ResolveRequired<TService>(object key) where TService : notnull => _scope.ResolveKeyed<TService>(key);
    }
}
