using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Operation.Application.QueryServices;

public interface IOperationQueryService
{
    // Alertas
    Task<IEnumerable<AlertResource>> Handle(GetActiveAlertsQuery query, CancellationToken ct);
    Task<AlertConfigurationResource> Handle(GetAlertConfigurationQuery query, CancellationToken ct);
    
    // Recomendaciones
    Task<IEnumerable<OperationRecommendationResource>> Handle(GetOperationRecommendationQuery query, CancellationToken ct);
}