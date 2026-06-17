using WebStone.Khiputech.Platform.Operation.Application.QueryServices;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Operation.Domain.Repositories;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Transform;

namespace WebStone.Khiputech.Platform.Operation.Application.Internal.QueryServices;

public class OperationQueryService(
    IAlertRepository alertRepository,
    IAlertConfigurationRepository configRepository,
    IOperationRecommendationRepository recommendationRepository)  // ← Agregar
    : IOperationQueryService
{
    public async Task<IEnumerable<AlertResource>> Handle(GetActiveAlertsQuery query, CancellationToken ct)
    {
        var alerts = await alertRepository.GetActiveAlertsAsync(ct);
        return alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity);
    }

    public async Task<AlertConfigurationResource> Handle(GetAlertConfigurationQuery query, CancellationToken ct)
    {
        var config = await configRepository.GetAsync(ct);
        if (config == null) throw new Exception("Configuration not found");
        return AlertConfigurationResourceFromEntityAssembler.ToResourceFromEntity(config);
    }
    
    public async Task<IEnumerable<OperationRecommendationResource>> Handle(GetOperationRecommendationQuery query, CancellationToken ct)
    {
        var recommendations = await recommendationRepository.GetAllAsync(ct);  // ← Usar el parámetro, no _recommendationRepository
        return recommendations.Select(OperationRecommendationResourceFromEntityAssembler.ToResourceFromEntity);
    }
}