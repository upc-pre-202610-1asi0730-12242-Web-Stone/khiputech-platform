using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Repositories;

public interface IRecommendationRepository
{
    Task<IEnumerable<OperationRecommendation>> GetAllAsync();
}