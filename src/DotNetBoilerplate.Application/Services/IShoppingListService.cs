using DotNetBoilerplate.Application.DTO.ShoppingList;

namespace DotNetBoilerplate.Application.Services;

public interface IShoppingListService
{
    Task CreateAsync(CreateShoppingListDto dto);
    Task AddProductAsync(Guid shoppingListId, AddProductDto dto);
    Task RemoveProductAsync(Guid shoppingListId, Guid productId);
    Task MarkProductAsBoughtAsync(Guid shoppingListId, Guid productId);
    Task MarkProductAsNotBoughtAsync(Guid shoppingListId, Guid productId);
    Task FinishShoppingListAsync(Guid shoppingListId);
    Task<IEnumerable<ShoppingListDto>> GetAllAsync();
}