using DotNetBoilerplate.Application.Organizations.Create;
using DotNetBoilerplate.Core.Organizations;
using DotNetBoilerplate.Core.Organizations.Exceptions;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Unit;

public class CreaeOrganiationHandlerTests
{
    [Fact]
    public async Task GivenNameIsNotUniqie_CreateOrganization_ShouldFail()
    {
        // Arrange
        var command = new CreateOrganizationCommand("Test Organization");
        var organizationsRepository = Substitute.For<IOrganizationsRepository>();
        organizationsRepository.IsOrganizationNameUniqueAsync(command.Name).Returns(false);

        var context = Substitute.For<IContext>();
        context.Identity.Id.Returns(Guid.NewGuid());

        var clock = Substitute.For<IClock>();
        clock.Now().Returns(DateTime.Now);


        var handler = new CreateOrganizationHandler(
            organizationsRepository,
            context,
            clock
        );

        // Act
        var act = async () => await handler.HandleAsync(command);

        // Assert
        act.ShouldThrow<OrganizationNameIsNotUniqueException>();
    }
}