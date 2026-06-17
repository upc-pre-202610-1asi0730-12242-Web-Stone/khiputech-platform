using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;

namespace WebStone.Khiputech.Platform.Operation.Application.Internal.QueryServices;

public interface IRecommendationQueryService
{
    Task<IEnumerable<OperationRecommendation>> Handle(GetOperationRecommendationQuery query);
}