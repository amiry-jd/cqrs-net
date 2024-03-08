namespace CQRS.NET {
    public interface IStrategicQueryHandler<in TQuery, TResult> : IQueryHandler<TQuery, TResult> { }

}