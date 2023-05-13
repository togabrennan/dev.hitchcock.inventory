using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MediatR;
using core.Domain;
using core.Commands;
using core.Queries;

namespace api.Controllers;


[ApiController]
[Route("[controller]")]
public class BinController : ControllerBase
{
    private readonly ISender _sender;

    public BinController(ISender sender) => _sender = sender;

    [HttpPost("{binId}/items")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Bin))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(string))]
    public async Task<ActionResult> AddItemToBin(
        [Required] [FromRoute] Guid binId, 
        [Required] [FromBody] Item item)
    {
        // Find the bin by id
        var bin = await _sender.Send(new GetBinDetailQuery(binId));

        if (bin == null)
            return BadRequest("Invalid Bin specified");

        // Save item to system
        await _sender.Send(new AddItemCommand(item));

        // Add the item to the bin
        var fullerBin = await _sender.Send(new AddItemToBinCommand(item, bin));

        // Return bin details
        return Ok(fullerBin);
    }

    [HttpPut("{binId}/items/{itemId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bin))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult> UpdateBinQuantity(
        [Required][FromRoute] Guid binId,
        [Required][FromQuery] int quantity,
        [Required][FromRoute] Guid itemId)
    {
        if (quantity < 0)
            return BadRequest("Total Quantity Must Be NonNegative");

        // Find the bin by id
        var bin = await _sender.Send(new GetBinDetailQuery(binId));

        if (bin == null)
            return BadRequest("Invalid Bin specified");

        // Find and update item quantity
        var item = await _sender.Send(new GetItemDetailQuery(itemId));

        if (item == null)
            return BadRequest("Invalid Item specified");

        item.Quantity = quantity;

        var modifiedBin = await _sender.Send(new ModifyBinItemCommand(bin, item));
        return Ok(modifiedBin);
    }


    [HttpDelete("{binId}/items/{itemId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bin))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult> RemoveItemFromBin(
        [Required][FromRoute] Guid binId,
        [Required][FromRoute] Guid itemId)
    {
        // Find the bin by id
        var bin = await _sender.Send(new GetBinDetailQuery(binId));

        if (bin == null)
            return BadRequest("Invalid Bin specified");

        // Remove Item from Bin
        var item = await _sender.Send(new GetItemDetailQuery(itemId));

        if (item == null)
            return BadRequest("Invalid Item specified");

        var modifiedBin = await _sender.Send(new RemoveItemFromBinCommand(bin, item));
        return Ok(modifiedBin);
    }

}

