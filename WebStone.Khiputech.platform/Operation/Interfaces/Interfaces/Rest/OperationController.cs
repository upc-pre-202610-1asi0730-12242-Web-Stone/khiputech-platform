using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebStone.Khiputech.Platform.Operation.Application.CommandServices;
using WebStone.Khiputech.Platform.Operation.Application.QueryServices;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

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
    public async Task<IActionResult> GetActiveAlerts(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetActiveAlertsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("configuration")]
    [SwaggerOperation(Summary = "Get alert configuration")]
    public async Task<IActionResult> GetConfiguration(CancellationToken ct)
    {
        var result = await queryService.Handle(new GetAlertConfigurationQuery(), ct);
        return Ok(result);
    }

    [HttpPut("configuration")]
    [SwaggerOperation(Summary = "Update alert configuration")]
    public async Task<IActionResult> UpdateConfiguration([FromBody] UpdateAlertConfigurationRequest request, CancellationToken ct)
    {
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
    public async Task<IActionResult> ActivateGeneralAlert([FromBody] ActivateAlertRequest request, CancellationToken ct)
    {
        var command = new CreateAlertCommand("General", "critica", request.Message, "operator");
        await commandService.Handle(command, ct);
        return Ok(new { message = "Alerta general activada" });
    }

    [HttpPost("alerts/{alertId}/resolve")]
    [SwaggerOperation(Summary = "Resolve an alert")]
    public async Task<IActionResult> ResolveAlert(int alertId, CancellationToken ct)
    {
        await commandService.Handle(new ResolveAlertCommand(alertId), ct);
        return NoContent();
    }
}

public record UpdateAlertConfigurationRequest(
    int ModerateThreshold,
    int CriticalThreshold,
    bool NotifyEmail,
    bool NotifyWhatsApp,
    bool NotifySms,
    bool NotifyPanel,
    string ContactCivilDefense);

public record ActivateAlertRequest(string Message);