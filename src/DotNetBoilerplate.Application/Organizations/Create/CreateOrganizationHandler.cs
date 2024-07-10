using DotNetBoilerplate.Core.Organizations;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Organizations.Create;

internal sealed class CreateOrganizationHandler(
    IOrganizationsRepository organizationsRepository,
    IContext context,
    IClock clock
) : ICommandHandler<CreateOrganizationCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateOrganizationCommand command)
    {
        var isNameUnique =
            await organizationsRepository.IsOrganizationNameUniqueAsync(command.Name);

        var organization = Organization.Create(
            command.Name,
            context.Identity.Id,
            clock.Now(),
            isNameUnique
        );

        await organizationsRepository.AddAsync(organization);

        return organization.Id;
    }
}