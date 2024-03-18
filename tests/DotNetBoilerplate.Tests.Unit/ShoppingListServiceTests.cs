using DotNetBoilerplate.Application.DTO.ShoppingList;
using DotNetBoilerplate.Application.Services;
using DotNetBoilerplate.Core.Entities.ShoppingLists;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DotNetBoilerplate.Tests.Unit;

public class ShoppingListServiceTests
{
    private readonly IClock _clock = Substitute.For<IClock>();
    private readonly IContext _context = Substitute.For<IContext>();
    private readonly IShoppingListRepository _repository = Substitute.For<IShoppingListRepository>();


    [Fact]
    public async Task CreateAsync_ShouldCreateShoppingList()
    {
        // Arrange
        var now = new DateTimeOffset(2024, 3, 3, 1, 1, 1, TimeSpan.Zero);

        var shoppingListService = new ShoppingListService(_context, _repository, _clock);
        var dto = new CreateShoppingListDto("Shopping List", now);

        _repository
            .ExistsForUserByShoppingDateAsync(_context.Identity.Id, dto.ShoppingDate)
            .Returns(false);

        // Act
        await shoppingListService.CreateAsync(dto);

        // Assert
        await _repository.Received(1).AddAsync(Arg.Any<ShoppingList>());
    }

    [Fact]
    public async Task GivenUserHasShoppingList_GetAllAsync_ShouldReturnShoppingList()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var now = new DateTimeOffset(2024, 3, 3, 1, 1, 1, TimeSpan.Zero);

        _clock.DateTimeOffsetNow().Returns(now);

        var shoppingList = ShoppingList.Create(
            userId,
            now,
            "Shopping List",
            now,
            false
        );

        _repository.GetByUserIdAsync(userId)
            .Returns([shoppingList]);

        _context.Identity.Id.Returns(userId);

        var shoppingListService = new ShoppingListService(_context, _repository, _clock);

        //Act
        var result = await shoppingListService.GetAllAsync();

        //Assert
        result.Count().ShouldBe(1);
        result.ShouldBeAssignableTo<IEnumerable<ShoppingListDto>>();
    }
}