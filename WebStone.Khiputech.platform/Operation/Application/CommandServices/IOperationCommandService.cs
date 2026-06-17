using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;

namespace WebStone.Khiputech.Platform.Operation.Application.CommandServices;

public interface IOperationCommandService
{
    // Alertas
    Task Handle(CreateAlertCommand command, CancellationToken ct);
    Task Handle(ResolveAlertCommand command, CancellationToken ct);
    Task Handle(UpdateAlertConfigurationCommand command, CancellationToken ct);
    
    // Recomendaciones
    Task<OperationRecommendation> Handle(CreateRecommendationCommand command, CancellationToken ct);
}