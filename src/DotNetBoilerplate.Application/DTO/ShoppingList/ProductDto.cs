using DotNetBoilerplate.Core.Entities.ShoppingLists;

namespace DotNetBoilerplate.Application.DTO.ShoppingList;

public sealed record ProductDto(
    Guid Id,
    string Name,
    int Quantity,
    ProductStatus Status,
    string Currency,
    int Amount
);