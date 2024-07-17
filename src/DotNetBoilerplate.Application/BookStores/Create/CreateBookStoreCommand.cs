using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.BookStores.Create;

public sealed record CreateBookStoreCommand(string Name, string Description) : ICommand;