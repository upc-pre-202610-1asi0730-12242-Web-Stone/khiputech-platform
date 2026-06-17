using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebStone.Khiputech.Platform.Capacity.Application.CommandServices;
using WebStone.Khiputech.Platform.Capacity.Application.QueryServices;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace WebStone.Khiputech.Platform.Capacity.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Capacity management (rooms and sensors)")]
public class CapacityController(
    ICapacityQueryService queryService,
    ICapacityCommandService commandService) : ControllerBase
{
    [HttpGet("rooms")]
    [SwaggerOperation(Summary = "Get all rooms with current occupancy")]
    public async Task<IActionResult> GetRooms(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetRoomsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("sensors")]
    [SwaggerOperation(Summary = "Get all sensors")]
    public async Task<IActionResult> GetSensors(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetSensorsQuery(), ct);
        return Ok(result);
    }

    [HttpPut("rooms/{roomId}/occupancy")]
    [SwaggerOperation(Summary = "Update room occupancy (for simulation or IoT integration)")]
    public async Task<IActionResult> UpdateOccupancy(int roomId, [FromBody] UpdateOccupancyRequest request, CancellationToken ct)
    {
        var command = new UpdateRoomOccupancyCommand(roomId, request.NewOccupancy);
        await commandService.Handle(command, ct);
        return NoContent();
    }
}

public record UpdateOccupancyRequest(int NewOccupancy);