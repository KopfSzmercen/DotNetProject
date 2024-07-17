namespace DotNetBoilerplate.Core.BookStores;

public interface IBookStoreRepository
{
    Task AddAsync(BookStore bookStore);

    Task<BookStore> GetByIdAsync(Guid id);

    Task UpdateAsync(BookStore bookStore);

    Task<bool> UserAlreadyOwnsOrganizationAsync(Guid ownerId);
}