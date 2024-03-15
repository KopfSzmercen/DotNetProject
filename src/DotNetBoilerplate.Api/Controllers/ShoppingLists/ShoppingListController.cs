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
}