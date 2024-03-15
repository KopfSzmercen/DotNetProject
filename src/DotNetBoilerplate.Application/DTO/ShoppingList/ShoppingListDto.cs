namespace DotNetBoilerplate.Application.DTO.ShoppingList;

public sealed record ShoppingListDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    string Name,
    DateTimeOffset ShoppingDate,
    DateTimeOffset? FinishedAt,
    IEnumerable<ProductDto> Products);