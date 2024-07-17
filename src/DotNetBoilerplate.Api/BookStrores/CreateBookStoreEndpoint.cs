using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.BookStores.Create;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.BookStrores;

public class CreateBookStoreEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("", Handle)
            .RequireAuthorization()
            .WithSummary("Create a new book store");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateBookStoreCommand(
            request.Name,
            request.Description
        );

        await commandDispatcher.DispatchAsync(command, ct);

        return TypedResults.Ok(new Response(Guid.NewGuid()));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Name { get; init; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Required] public string Description { get; init; }
    }
}