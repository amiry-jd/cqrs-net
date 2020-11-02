namespace CQRS.NET {
    public interface ICommandHandlerDecorator<TCommand> where TCommand : ICommand {

        ICommandHandler<TCommand> Decoratee { get; }

    }
}