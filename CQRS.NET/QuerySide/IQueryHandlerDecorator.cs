namespace CQRS.NET {
    public interface IQueryHandlerDecorator<in TQuery, TResult> where TQuery : IQuery<TResult> {

        IQueryHandler<TQuery, TResult> Decoratee { get; }

    }
}