using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Organizations.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace DotNetBoilerplate.Api.Organizations;

internal sealed class CreateOrganizationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create organization");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateOrganizationCommand(request.Name);

        var result = await commandDispatcher.DispatchAsync<CreateOrganizationCommand, Guid>(command, ct);

        return TypedResults.Ok(new Response(result));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        [Required] public string Name { get; init; }
    }
}