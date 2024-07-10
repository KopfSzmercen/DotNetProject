using DotNetBoilerplate.Core.Organizations.Exceptions;

namespace DotNetBoilerplate.Core.Organizations;

public class Organization
{
    private Organization()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid OwnerId { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public static Organization Create(
        string name,
        Guid ownerId,
        DateTimeOffset now,
        bool nameIsUnique
    )
    {
        if (!nameIsUnique)
            throw new OrganizationNameIsNotUniqueException();

        return new Organization
        {
            Id = Guid.NewGuid(),
            Name = name,
            OwnerId = ownerId,
            CreatedAt = now
        };
    }
}