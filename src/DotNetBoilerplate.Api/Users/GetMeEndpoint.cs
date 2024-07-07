using DotNetBoilerplate.Application.Users;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Users;

internal sealed class GetMeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("me", Handle)
            .RequireAuthorization()
            .WithSummary("Get Me");
    }

    private static async Task<Results<Ok<Response>, NotFound>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        [FromServices] IContext context,
        CancellationToken ct
    )
    {
        var query = new GetUserQuery(context.Identity.Id);
        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        var response = new Response
        {
            Id = result.Id,
            Username = result.Username,
            Email = result.Email,
            Role = result.Role
        };

        return TypedResults.Ok(response)!;
    }

    public sealed record Response
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}