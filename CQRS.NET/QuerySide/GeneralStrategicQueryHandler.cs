using System;
using System.Threading.Tasks;

namespace CQRS.NET {
    public class GeneralStrategicQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> {

        private readonly IStrategyDetector<TQuery> _detector;
        private readonly IExtendedServiceProvider _serviceProvider;


        public GeneralStrategicQueryHandler(
            IStrategyDetector<TQuery> detector, IExtendedServiceProvider serviceProvider) {
            _detector = detector;
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> HandleAsync(TQuery command) {
            var strategyType = _detector.FindHandlerType(command);

            //var strategy     = _provider.GetRequiredService(strategyType) as ICommandHandler<TCommand>;

            var s2 = _serviceProvider.ResolveRequired<IQueryHandler<TQuery, TResult>>(key: strategyType.FullName);

            if (s2 == null) throw new InvalidOperationException();

            return await s2.HandleAsync(command);
        }

    }
}