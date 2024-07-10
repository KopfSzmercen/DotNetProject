using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Organizations.Exceptions;

internal sealed class OrganizationNameIsNotUniqueException() : CustomException("Organization name is not unique.");