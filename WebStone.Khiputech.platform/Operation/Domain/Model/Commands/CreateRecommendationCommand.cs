namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;

public record CreateRecommendationCommand(
    string RoomName,
    string Issue,
    string SuggestedAction
);