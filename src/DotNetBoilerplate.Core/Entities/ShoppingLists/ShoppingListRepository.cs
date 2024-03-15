namespace DotNetBoilerplate.Core.Entities.ShoppingLists;

public interface IShoppingListRepository
{
    Task AddAsync(ShoppingList shoppingList);
    Task<ShoppingList> GetAsync(Guid id);
    Task<bool> ExistsForUserByShoppingDateAsync(Guid userId, DateTimeOffset shoppingDate);
    Task UpdateAsync(ShoppingList shoppingList);
    Task<IEnumerable<ShoppingList>> GetByUserIdAsync(Guid userId);
}