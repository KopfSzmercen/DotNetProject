using DotNetBoilerplate.Application.DTO.ShoppingList;
using DotNetBoilerplate.Application.Services;
using DotNetBoilerplate.Shared.Abstractions.Exceptions.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DotNetBoilerplate.Api.Controllers.ShoppingLists;

[Route("shopping-lists")]
[Authorize]
public class ShoppingListsController(
    IShoppingListService shoppingListService
) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create a new shopping list")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Post(
        [FromBody] CreateShoppingListDto dto
    )
    {
        await shoppingListService.CreateAsync(dto);
        return Created();
    }

    [HttpGet]
    [SwaggerOperation("Get all shopping lists")]
    [ProducesResponseType(typeof(IEnumerable<ShoppingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ShoppingListDto>>> GetAll()
    {
        var shoppingLists = await shoppingListService.GetAllAsync();
        return Ok(shoppingLists);
    }

    [HttpPost("{shoppingListId:guid}/products")]
    [SwaggerOperation("Add a product to a shopping list")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PostProduct(
        [FromRoute] Guid shoppingListId,
        [FromBody] AddProductDto dto
    )
    {
        await shoppingListService.AddProductAsync(shoppingListId, dto);
        return Created();
    }

    [HttpDelete("{shoppingListId:guid}/products/{productId:guid}")]
    [SwaggerOperation("Remove a product from a shopping list")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteProduct(
        [FromRoute] Guid shoppingListId,
        [FromRoute] Guid productId
    )
    {
        await shoppingListService.RemoveProductAsync(shoppingListId, productId);
        return NoContent();
    }

    [HttpPatch("{shoppingListId:guid}/products/{productId:guid}/status")]
    [SwaggerOperation("Update product status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> PatchProductStatus(
        [FromRoute] Guid shoppingListId,
        [FromRoute] Guid productId,
        [FromBody] UpdateProductStatusDto dto
    )
    {
        await shoppingListService.UpdateProductStatusAsync(shoppingListId, productId, dto.Status);
        return NoContent();
    }
}