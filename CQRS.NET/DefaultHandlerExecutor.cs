using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CQRS.NET {
    public sealed class DefaultHandlerExecutor : IHandlerExecutor {

        private readonly IServiceProvider _serviceProvider;

        public DefaultHandlerExecutor(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query) {
            var serviceType = TypeCache.GetOrAdd(query.GetType(),
                                                 t => typeof(IQueryHandler<,>).MakeGenericType(t, typeof(TResult)));

            dynamic service = _serviceProvider.GetService(serviceType);

            if (service == null) throw new HandlerNotFoundException(query.GetType(), serviceType);

            var result = await service.HandleAsync((dynamic)query);

            return result;
        }

        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult> {
            var serviceType = TypeCache.GetOrAdd(query.GetType(),
                t => typeof(IQueryHandler<,>).MakeGenericType(t, typeof(TResult)));

            var serviceObj = _serviceProvider.GetService(serviceType);
            if (!(serviceObj is IQueryHandler<TQuery, TResult> service))
                throw new HandlerNotFoundException(query.GetType(), serviceType);
            var result = await service.HandleAsync(query);
            return result;
        }

        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand {
            var serviceType = TypeCache.GetOrAdd(command.GetType(),
                t => typeof(ICommandHandler<>).MakeGenericType(t));

            var serviceObj = _serviceProvider.GetService(serviceType);
            if (!(serviceObj is ICommandHandler<TCommand> service))
                throw new HandlerNotFoundException(command.GetType(), serviceType);
            await service.HandleAsync(command);
        }

        #region Types Cache

        private static readonly ConcurrentDictionary<Type, Type> TypeCache
            = new ConcurrentDictionary<Type, Type>();

        #endregion
    }
}