using System.Threading.Tasks;

namespace CQRS.NET {
    public interface IHandlerExecutor {

        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

    }
}