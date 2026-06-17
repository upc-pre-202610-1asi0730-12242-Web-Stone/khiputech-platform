using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebStone.Khiputech.Platform.Operation.Application.CommandServices;
using WebStone.Khiputech.Platform.Operation.Application.QueryServices;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Transform;

namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Operations management (alerts and civil defense)")]
public class OperationController(
    IOperationQueryService queryService,
    IOperationCommandService commandService) : ControllerBase
{
    [HttpGet("alerts/active")]
    [SwaggerOperation(Summary = "Get active alerts")]
    [ProducesResponseType(typeof(IEnumerable<AlertResource>), 200)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetActiveAlerts(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetActiveAlertsQuery(), ct);
        
        if (!result.Any())
            return Ok(new { message = "No hay alertas activas", data = result });
        
        return Ok(result);
    }

    [HttpGet("configuration")]
    [SwaggerOperation(Summary = "Get alert configuration")]
    [ProducesResponseType(typeof(AlertConfigurationResource), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetConfiguration(CancellationToken ct)
    {
        try
        {
            var result = await queryService.Handle(new GetAlertConfigurationQuery(), ct);
            return Ok(result);
        }
        catch (Exception)
        {
            return NotFound(new { message = "Configuración no encontrada" });
        }
    }

    [HttpPut("configuration")]
    [SwaggerOperation(Summary = "Update alert configuration")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateConfiguration([FromBody] UpdateAlertConfigurationRequest request, CancellationToken ct)
    {
        if (request.ModerateThreshold >= request.CriticalThreshold)
            return BadRequest(new { message = "El umbral crítico debe ser mayor que el umbral moderado" });
        
        var command = new UpdateAlertConfigurationCommand(
            request.ModerateThreshold,
            request.CriticalThreshold,
            request.NotifyEmail,
            request.NotifyWhatsApp,
            request.NotifySms,
            request.NotifyPanel,
            request.ContactCivilDefense);
        await commandService.Handle(command, ct);
        return NoContent();
    }

    [HttpPost("alerts/activate-general")]
    [SwaggerOperation(Summary = "Activate general alert (simulate emergency)")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ActivateGeneralAlert([FromBody] ActivateAlertRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            return BadRequest(new { message = "El mensaje de la alerta es requerido" });
        
        var command = new CreateAlertCommand("General", "critica", request.Message, "operator");
        await commandService.Handle(command, ct);
        return Ok(new { message = "Alerta general activada exitosamente" });
    }

    [HttpPost("alerts/{alertId}/resolve")]
    [SwaggerOperation(Summary = "Resolve an alert")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ResolveAlert(int alertId, CancellationToken ct)
    {
        try
        {
            await commandService.Handle(new ResolveAlertCommand(alertId), ct);
            return Ok(new { message = $"Alerta {alertId} resuelta correctamente" });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet("recommendations")]
    [SwaggerOperation(Summary = "Get all operation recommendations")]
    [ProducesResponseType(typeof(IEnumerable<OperationRecommendationResource>), 200)]
    public async Task<IActionResult> GetRecommendations(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetOperationRecommendationQuery(), ct);
        return Ok(result);
    }

    [HttpPost("recommendations")]
    [SwaggerOperation(Summary = "Create a new recommendation")]
    [ProducesResponseType(typeof(OperationRecommendationResource), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateRecommendation([FromBody] CreateRecommendationRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.RoomName) || string.IsNullOrWhiteSpace(request.Issue))
            return BadRequest(new { message = "RoomName e Issue son requeridos" });
    
        var command = new CreateRecommendationCommand(request.RoomName, request.Issue, request.SuggestedAction);
        var result = await commandService.Handle(command, ct);
    
        var resource = OperationRecommendationResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetRecommendations), new { id = result.Id }, resource);
    }
}