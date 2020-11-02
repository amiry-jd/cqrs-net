using System.Threading.Tasks;

namespace CQRS.NET {
    public interface ICommandHandler<in TCommand> where TCommand : ICommand {

        Task HandleAsync(TCommand command);

    }
}