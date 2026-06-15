using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Repositories;

namespace WebStone.Khiputech.Platform.Operation.Application.Internal.QueryServices;

public class RecommendationQueryService : IRecommendationQueryService
{
    private readonly IRecommendationRepository _repository;

    public RecommendationQueryService(IRecommendationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OperationRecommendation>> Handle(GetOperationRecommendationsQuery query)
    {
        return await _repository.GetAllAsync();
    }
}