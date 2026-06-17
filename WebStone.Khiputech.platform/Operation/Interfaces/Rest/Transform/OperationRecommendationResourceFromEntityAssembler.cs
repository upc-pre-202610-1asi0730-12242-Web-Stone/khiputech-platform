using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Transform;

public static class OperationRecommendationResourceFromEntityAssembler
{
    public static OperationRecommendationResource ToResourceFromEntity(OperationRecommendation recommendation)
    {
        return new OperationRecommendationResource(
            recommendation.Id,
            recommendation.RoomName,
            recommendation.Issue,
            recommendation.SuggestedAction,
            recommendation.GeneratedAt
        );
    }
}