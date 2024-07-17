using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.BookStores.Create;

internal sealed class CreateBookStoreHandler(
    IClock clock,
    IContext context,
    IBookStoreRepository bookStoreRepository
) : ICommandHandler<CreateBookStoreCommand>
{
    public async Task HandleAsync(CreateBookStoreCommand command)
    {
        var userAlreadyOwnsOrganization =
            await bookStoreRepository.UserAlreadyOwnsOrganizationAsync(context.Identity.Id);

        var bookStore = BookStore.Create(
            command.Name,
            command.Description,
            clock.Now(),
            context.Identity.Id,
            userAlreadyOwnsOrganization
        );

        await bookStoreRepository.AddAsync(bookStore);
    }
}