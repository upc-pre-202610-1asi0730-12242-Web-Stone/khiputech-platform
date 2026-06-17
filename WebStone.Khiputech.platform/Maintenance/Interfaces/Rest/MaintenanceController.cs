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
[SwaggerTag("Artwork maintenance and restoration")]
public class MaintenanceController(
    IMaintenanceQueryService queryService,
    IMaintenanceCommandService commandService) : ControllerBase
{
    [HttpGet("tasks")]
    [SwaggerOperation(Summary = "Get all maintenance tasks (optional filter by active status)")]
    public async Task<IActionResult> GetTasks([FromQuery] bool? activeOnly, CancellationToken ct)
    {
        var result = await queryService.Handle(new GetMaintenanceTasksQuery(activeOnly), ct);
        return Ok(result);
    }

    [HttpGet("tasks/{id}")]
    [SwaggerOperation(Summary = "Get a maintenance task by ID")]
    public async Task<IActionResult> GetTaskById(int id, CancellationToken ct)
    {
        var result = await queryService.Handle(new GetMaintenanceTaskByIdQuery(id), ct);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("artworks/blocked")]
    [SwaggerOperation(Summary = "Get all artworks currently under maintenance (QR/NFC blocked)")]
    public async Task<IActionResult> GetBlockedArtworks(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetBlockedArtworksQuery(), ct);
        return Ok(result);
    }

    [HttpPost("tasks/schedule")]
    [SwaggerOperation(Summary = "Schedule a new maintenance task")]
    public async Task<IActionResult> ScheduleMaintenance([FromBody] ScheduleMaintenanceRequest request, CancellationToken ct)
    {
        var command = new ScheduleMaintenanceCommand(
            request.ArtworkId,
            request.ArtworkName,
            request.StartDate.ToUniversalTime(),
            request.EndDate.ToUniversalTime(),
            request.Reason
        );
        var task = await commandService.Handle(command, ct);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPost("artworks/{artworkId}/restore")]
    [SwaggerOperation(Summary = "Restore an artwork after maintenance (complete the task)")]
    public async Task<IActionResult> RestoreArtwork(int artworkId, CancellationToken ct)
    {
        // Ahora pasamos el ArtworkId directamente
        var command = new RestoreArtworkAvailabilityCommand(artworkId);
        await commandService.Handle(command, ct);
        return Ok(new { message = $"Obra {artworkId} restaurada exitosamente" });
    }

    [HttpPost("tasks/{taskId}/cancel")]
    [SwaggerOperation(Summary = "Cancel a scheduled maintenance task")]
    public async Task<IActionResult> CancelTask(int taskId, CancellationToken ct)
    {
        await commandService.Handle(new CancelMaintenanceCommand(taskId), ct);
        return NoContent();
    }
}