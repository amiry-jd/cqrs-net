using System;
using System.Threading.Tasks;

namespace CQRS.NET.CommandSide {
    public class GeneralStrategicCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand {

        private readonly IExtendedServiceProvider _provider;
        private readonly IStrategyDetector<TCommand> _detector;


        public GeneralStrategicCommandHandler(IExtendedServiceProvider provider, IStrategyDetector<TCommand> detector) {
            _provider = provider;
            _detector = detector;
        }

        public async Task HandleAsync(TCommand command) {
            var strategyType = _detector.FindHandlerType(command);

            var service = _provider.ResolveRequired<ICommandHandler<TCommand>>(key: strategyType.FullName);

            if (service == null) throw new InvalidOperationException();

            await service.HandleAsync(command);
        }

    }
}