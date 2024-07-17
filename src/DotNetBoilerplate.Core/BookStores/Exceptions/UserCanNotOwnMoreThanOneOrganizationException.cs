using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.BookStores.Exceptions;

internal sealed class UserCanNotOwnMoreThanOneOrganizationException()
    : CustomException("User can not own more than one organization.")
{
};