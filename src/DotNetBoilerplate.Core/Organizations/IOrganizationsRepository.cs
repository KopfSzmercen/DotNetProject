namespace DotNetBoilerplate.Core.Organizations;

public interface IOrganizationsRepository
{
    Task<Organization?> GetByIdAsync(Guid id);

    Task AddAsync(Organization organization);

    Task UpdateAsync(Organization organization);

    Task DeleteAsync(Organization organization);

    Task<bool> IsOrganizationNameUniqueAsync(string name);
}