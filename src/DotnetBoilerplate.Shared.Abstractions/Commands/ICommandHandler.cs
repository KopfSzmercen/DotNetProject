namespace DotNetBoilerplate.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand> where TCommand : class
{
    Task HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult> where TCommand : class
{
    Task<TResult> HandleAsync(TCommand command);
}