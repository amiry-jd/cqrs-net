using System.Threading.Tasks;

namespace CQRS.NET {
    public interface IQueryHandler<in TQuery, TResult> {
        Task<TResult> HandleAsync(TQuery query);
    }
}
