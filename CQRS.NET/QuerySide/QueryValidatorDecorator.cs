using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.NET {
    public class QueryValidatorDecorator<TQuery, TResult>
        : IQueryHandlerDecorator<TQuery, TResult>,
          IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> {

        private readonly IExtendedServiceProvider _serviceProvider;
        private readonly IQueryHandler<TQuery, TResult> _decoratee;

        public QueryValidatorDecorator(IQueryHandler<TQuery, TResult> decoratee, IExtendedServiceProvider serviceProvider) {
            _decoratee = decoratee;
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> HandleAsync(TQuery query) {
            var validators = GetValidator();
            foreach (var validator in validators)
                await validator.ValidateAsync(query);
            return await _decoratee.HandleAsync(query);
        }

        private IEnumerable<IValidator<TQuery>> GetValidator() {
            var handler = _decoratee;
            var dec = _decoratee as IQueryHandlerDecorator<TQuery, TResult>;

            while (dec != null) {
                handler = dec.Decoratee;
                dec = dec.Decoratee as IQueryHandlerDecorator<TQuery, TResult>;
            }

            var attr = handler.GetType().GetCustomAttribute(typeof(ValidatorAttribute)) as ValidatorAttribute;

            if (attr == null) {
                var validator = _serviceProvider.ResolveOptional<IValidator<TQuery>>();
                if (validator == null)
                    throw new ValidatorNotFoundException(typeof(TQuery), typeof(IValidator<TQuery>));
                return new[] { validator };
            }

            var list = new List<IValidator<TQuery>>();
            foreach (var validatorType in attr.ValidatorTypes) {
                var validator = _serviceProvider.ResolveOptional<IValidator<TQuery>>(key: validatorType.FullName);

                if (validator == null)
                    throw new ValidatorNotFoundException(typeof(TQuery), typeof(IValidator<TQuery>),
                                                              validatorType.FullName);
                list.Add(validator);
            }
            return list;
        }

        public IQueryHandler<TQuery, TResult> Decoratee => _decoratee;

    }
}