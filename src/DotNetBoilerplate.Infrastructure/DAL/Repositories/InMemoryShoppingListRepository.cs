using DotNetBoilerplate.Core.Entities.ShoppingLists;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryShoppingListRepository : IShoppingListRepository
{
    private readonly List<ShoppingList> _shoppingLists = [];

    public Task AddAsync(ShoppingList shoppingList)
    {
        _shoppingLists.Add(shoppingList);
        return Task.CompletedTask;
    }

    public Task<ShoppingList> GetAsync(Guid id)
    {
        var shoppingList = _shoppingLists.SingleOrDefault(x => x.Id == id);

        return Task.FromResult(shoppingList);
    }

    public Task<bool> ExistsForUserByShoppingDateAsync(Guid userId, DateTimeOffset shoppingDate)
    {
        var exists = _shoppingLists
            .Any(x => x.UserId == userId && x.ShoppingDate == shoppingDate);

        return Task.FromResult(exists);
    }

    public Task UpdateAsync(ShoppingList shoppingList)
    {
        var index = _shoppingLists.FindIndex(x => x.Id == shoppingList.Id);
        _shoppingLists[index] = shoppingList;

        return Task.CompletedTask;
    }

    public Task<IEnumerable<ShoppingList>> GetByUserIdAsync(Guid userId)
    {
        var shoppingLists = _shoppingLists
            .Where(x => x.UserId == userId)
            .ToList();

        return Task.FromResult(shoppingLists.AsEnumerable());
    }
}