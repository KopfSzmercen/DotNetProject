using DotNetBoilerplate.Application.DTO.ShoppingList;
using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Entities.ShoppingLists;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Services;

internal sealed class ShoppingListService(
    IContext context,
    IShoppingListRepository repository,
    IClock clock)
    : IShoppingListService
{
    public async Task CreateAsync(CreateShoppingListDto dto)
    {
        var hasShoppingListWithSameShoppingDate = await repository
            .ExistsForUserByShoppingDateAsync(context.Identity.Id, dto.ShoppingDate);

        var shoppingList = ShoppingList.Create(
            context.Identity.Id,
            clock.DateTimeOffsetNow(),
            dto.Name,
            dto.ShoppingDate,
            hasShoppingListWithSameShoppingDate);

        await repository.AddAsync(shoppingList);
    }

    public async Task AddProductAsync(Guid shoppingListId, AddProductDto dto)
    {
        var shoppingList = await repository.GetAsync(shoppingListId);

        if (shoppingList is null || shoppingList.UserId != context.Identity.Id)
            throw new ShoppingListNotFoundException(shoppingListId);

        var product = Product.Create(dto.Name, dto.Quantity, Money.Create(dto.Currency, dto.Amount));

        shoppingList.AddProduct(product);

        await repository.UpdateAsync(shoppingList);
    }

    public async Task RemoveProductAsync(Guid shoppingListId, Guid productId)
    {
        var shoppingList = await repository.GetAsync(shoppingListId);

        if (shoppingList is null || shoppingList.UserId != context.Identity.Id)
            throw new ShoppingListNotFoundException(shoppingListId);

        shoppingList.RemoveProduct(productId);


        await repository.UpdateAsync(shoppingList);
    }

    public async Task UpdateProductStatusAsync(Guid shoppingListId, Guid productId, ProductStatus status)
    {
        var shoppingList = await repository.GetAsync(shoppingListId);

        if (shoppingList is null || shoppingList.UserId != context.Identity.Id)
            throw new ShoppingListNotFoundException(shoppingListId);

        switch (status)
        {
            case ProductStatus.Bought:
                shoppingList.MarkProductAsBought(productId);
                break;
            case ProductStatus.NotBought:
                shoppingList.MarkProductAsNotBought(productId);
                break;
        }

        await repository.UpdateAsync(shoppingList);
    }

    public async Task FinishShoppingListAsync(Guid shoppingListId)
    {
        var shoppingList = await repository.GetAsync(shoppingListId);

        if (shoppingList is null || shoppingList.UserId != context.Identity.Id)
            throw new ShoppingListNotFoundException(shoppingListId);

        shoppingList.Finish(clock.DateTimeOffsetNow());

        await repository.UpdateAsync(shoppingList);
    }

    public async Task<IEnumerable<ShoppingListDto>> GetAllAsync()
    {
        var shoppingLists = await repository.GetByUserIdAsync(context.Identity.Id);

        return shoppingLists.Select(x => new ShoppingListDto(
            x.Id,
            x.CreatedAt,
            x.Name,
            x.ShoppingDate,
            x.FinishedAt,
            x.Products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Quantity,
                p.Status,
                p.Price.Currency,
                p.Price.Amount)))
        );
    }
}