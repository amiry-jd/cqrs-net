namespace CQRS.NET {
    public interface IStrategicCommandHandler<in TCommand> : ICommandHandler<TCommand> where TCommand : ICommand { }

}