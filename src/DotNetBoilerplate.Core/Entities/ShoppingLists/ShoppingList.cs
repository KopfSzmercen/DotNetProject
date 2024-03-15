using DotNetBoilerplate.Core.Exceptions;

namespace DotNetBoilerplate.Core.Entities.ShoppingLists;

public class ShoppingList
{
    private const int MaxNumberOfProducts = 20;
    private List<Product> _products = [];

    private ShoppingList()
    {
    }

    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public DateTimeOffset CreatedAt { get; private init; }
    public string Name { get; private set; }
    public DateTimeOffset ShoppingDate { get; private init; }
    public IEnumerable<Product> Products => _products;

    public DateTimeOffset? FinishedAt { get; private set; }

    public static ShoppingList Create(
        Guid userId,
        DateTimeOffset createdAt,
        string name,
        DateTimeOffset shoppingDate,
        bool userHasShoppingListWithSameShoppingDate
    )
    {
        if (userHasShoppingListWithSameShoppingDate)
            throw new UserHasShoppingListWithSameShoppingDateException();

        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = createdAt,
            Name = name,
            ShoppingDate = shoppingDate,
            _products = []
        };

        return shoppingList;
    }

    public void AddProduct(Product product)
    {
        if (_products.Count >= MaxNumberOfProducts)
            throw new MaxNumberOfProductsExceededException();

        _products.Add(product);
    }

    public void RemoveProduct(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (product is null)
            throw new ProductNotFound();

        _products.Remove(product);
    }

    public void MarkProductAsBought(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (product is null)
            throw new ProductNotFound();

        product.MarkAsBought();
    }

    public void MarkProductAsNotBought(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (product is null)
            throw new ProductNotFound();

        product.MarkAsNotBought();
    }

    public void Finish(DateTimeOffset now)
    {
        var allProductsAreBoughtOrNotBought = _products
            .All(p => p.Status is ProductStatus.Bought or ProductStatus.NotBought);

        if (!allProductsAreBoughtOrNotBought)
            throw new AllProductsAreNotBoughtOrBoughtException();

        FinishedAt = now;
    }
}