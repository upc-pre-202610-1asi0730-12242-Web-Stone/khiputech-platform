using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Domain.Repositories;

public interface IOperationRecommendationRepository
{
    Task<IEnumerable<OperationRecommendation>> GetAllAsync(CancellationToken ct);
    Task AddAsync(OperationRecommendation recommendation, CancellationToken ct);
}