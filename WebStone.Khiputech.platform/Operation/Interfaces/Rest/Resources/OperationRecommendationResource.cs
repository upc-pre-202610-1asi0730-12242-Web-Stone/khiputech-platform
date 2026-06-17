namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

public record OperationRecommendationResource(
    int Id,
    string RoomName,
    string Issue,
    string SuggestedAction,
    DateTime GeneratedAt
);