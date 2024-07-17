using DotNetBoilerplate.Application.BookStores.Create;
using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;
using NSubstitute;
using Xunit;

namespace DotNetBoilerplate.Tests.Unit;

public class CreateBookStoreHandlerTests
{
    private static readonly IClock Clock = Substitute.For<IClock>();
    private static readonly IContext Context = Substitute.For<IContext>();
    private static readonly IBookStoreRepository BookStoreRepository = Substitute.For<IBookStoreRepository>();


    [Fact]
    public async Task GivenUserDoesNotOwnBookStore_HandleAsyncShouldSuceed_AndBookStoreRepositoryShouldBeCalled()
    {
        // Arrange 
        var sut = new CreateBookStoreHandler(Clock, Context, BookStoreRepository);
        var userId = Guid.NewGuid();

        Context.Identity.Id.Returns(userId);
        BookStoreRepository.UserAlreadyOwnsOrganizationAsync(userId).Returns(false);
        Clock.Now().Returns(DateTime.Now);

        // Act
        await sut.HandleAsync(new CreateBookStoreCommand("BookStoreName", "BookStoreDescription"));

        // Assert
        await BookStoreRepository.Received(1).AddAsync(Arg.Any<BookStore>());
    }
}