namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

public record CreateRecommendationRequest(
    string RoomName,
    string Issue,
    string SuggestedAction
);