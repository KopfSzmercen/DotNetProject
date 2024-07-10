using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Organizations.Create;

public sealed record CreateOrganizationCommand(string Name) : ICommand<Guid>;