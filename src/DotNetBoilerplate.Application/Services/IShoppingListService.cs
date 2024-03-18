using DotNetBoilerplate.Application.DTO.ShoppingList;
using DotNetBoilerplate.Core.Entities.ShoppingLists;

namespace DotNetBoilerplate.Application.Services;

public interface IShoppingListService
{
    Task CreateAsync(CreateShoppingListDto dto);
    Task AddProductAsync(Guid shoppingListId, AddProductDto dto);

    Task RemoveProductAsync(Guid shoppingListId, Guid productId);

    // Task MarkProductAsBoughtAsync(Guid shoppingListId, Guid productId);
    // Task MarkProductAsNotBoughtAsync(Guid shoppingListId, Guid productId);
    Task UpdateProductStatusAsync(Guid shoppingListId, Guid productId, ProductStatus status);
    Task FinishShoppingListAsync(Guid shoppingListId);
    Task<IEnumerable<ShoppingListDto>> GetAllAsync();
}