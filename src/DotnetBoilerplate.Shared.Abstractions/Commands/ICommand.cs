namespace DotNetBoilerplate.Shared.Abstractions.Commands;

public interface ICommand
{
}

public interface ICommand<TResult> : ICommand
{
}