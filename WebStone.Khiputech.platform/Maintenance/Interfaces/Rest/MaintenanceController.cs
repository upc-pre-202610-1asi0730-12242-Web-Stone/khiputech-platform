using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebStone.Khiputech.Platform.Maintenance.Application.CommandServices;
using WebStone.Khiputech.Platform.Maintenance.Application.QueryServices;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Maintenance management (artwork conservation)")]
public class MaintenanceController(
    IMaintenanceQueryService queryService,
    IMaintenanceCommandService commandService) : ControllerBase
{
    [HttpGet("tasks")]
    [SwaggerOperation(Summary = "Get maintenance tasks, optionally filtered by status")]
    public async Task<IActionResult> GetTasks([FromQuery] string? status, CancellationToken ct)
    {
        var result = await queryService.Handle(new GetMaintenanceTasksQuery(status), ct);
        return Ok(result);
    }

    [HttpPost("tasks/schedule")]
    [SwaggerOperation(Summary = "Schedule maintenance for an artwork")]
    public async Task<IActionResult> ScheduleMaintenance([FromBody] ScheduleMaintenanceRequest request, CancellationToken ct)
    {
        var command = new ScheduleMaintenanceCommand(request.ArtworkId, request.ArtworkName, request.Reason, request.ScheduledBy);
        await commandService.Handle(command, ct);
        return Ok(new { message = "Maintenance scheduled successfully" });
    }

    [HttpPost("tasks/{taskId}/restore")]
    [SwaggerOperation(Summary = "Restore artwork availability after maintenance is completed")]
    public async Task<IActionResult> RestoreArtwork(int taskId, CancellationToken ct)
    {
        var command = new RestoreArtworkAvailabilityCommand(taskId);
        await commandService.Handle(command, ct);
        return Ok(new { message = "Artwork availability restored" });
    }

    [HttpGet("artworks/blocked")]
    [SwaggerOperation(Summary = "Get IDs of artworks currently under maintenance")]
    public async Task<IActionResult> GetBlockedArtworks(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetBlockedArtworksQuery(), ct);
        return Ok(result);
    }
}

public record ScheduleMaintenanceRequest(int ArtworkId, string ArtworkName, string Reason, string ScheduledBy);