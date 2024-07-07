using DotNetBoilerplate.Application.DTO;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Users;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserDetailsDto?>
{
}