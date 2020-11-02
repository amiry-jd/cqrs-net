using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.NET {
    public class CommandValidatorDecorator<TCommand>
        : ICommandHandlerDecorator<TCommand>, ICommandHandler<TCommand> where TCommand : ICommand {

        private readonly IExtendedServiceProvider _serviceProvider;
        private readonly ICommandHandler<TCommand> _decoratee;

        public CommandValidatorDecorator(
            ICommandHandler<TCommand> decoratee, IExtendedServiceProvider serviceProvider) {
            _decoratee = decoratee;
            _serviceProvider = serviceProvider;
        }

        async Task ICommandHandler<TCommand>.HandleAsync(TCommand command) {
            var validators = GetValidator();
            foreach (var validator in validators)
                await validator.ValidateAsync(command);
            await _decoratee.HandleAsync(command);
        }

        private IEnumerable<IValidator<TCommand>> GetValidator() {
            var handler = _decoratee;
            var dec = _decoratee as ICommandHandlerDecorator<TCommand>;

            while (dec != null) {
                handler = dec.Decoratee;
                dec = dec.Decoratee as ICommandHandlerDecorator<TCommand>;
            }

            var attr = handler.GetType().GetCustomAttribute(typeof(ValidatorAttribute)) as ValidatorAttribute;

            if (attr == null) {
                var validator = _serviceProvider.ResolveOptional<IValidator<TCommand>>();
                if (validator == null)
                    throw new InvalidOperationException($"Cannot find a validator class that implements {typeof(IValidator<TCommand>)}.");
                return new[] { validator };
            }

            var list = new List<IValidator<TCommand>>();
            foreach (var validatorType in attr.ValidatorTypes) {
                var validator = _serviceProvider.ResolveOptional<IValidator<TCommand>>(key: validatorType.FullName);
                if (validator == null)
                    throw new
                        InvalidOperationException($"Cannot find a validator class named {validatorType.FullName} that implements {typeof(IValidator<TCommand>)}.");
                list.Add(validator);
            }
            return list;
        }

        public ICommandHandler<TCommand> Decoratee => _decoratee;

    }
}