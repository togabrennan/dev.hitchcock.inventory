using MediatR;
using core.Domain;
using core.Commands;
using core.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ISender _sender;

    public ItemController(ISender sender) => _sender = sender;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Item))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult> AddItem(Item item)
    {
        var addedItem = await _sender.Send(new AddItemCommand(item));
        return Ok(addedItem);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Item))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult> ModifyItem(Item item)
    {
        // There is some funkiness here that I'd need a bit more time to look
        // at the behavior for. Due to the structure of the Bin holding
        // actual items insetad of references, when I update the items listing
        // we'd have to do an expensive search through bins and cascade those
        // changes manually in this exercise. This could be handled nicely in
        // production with appropriate table design / referential integrity
        // rules
        var modifiedItem = await _sender.Send(new AddItemCommand(item));
        return Ok(modifiedItem);
    }

    [HttpDelete("{itemId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bin))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult> DeleteItem([Required][FromRoute] Guid itemId)
    {
        var result = await _sender.Send(new DeleteItemCommand(itemId));
        if (result)
            return Ok();
        else
            return BadRequest("An error occurred while deleting item");
    }
}