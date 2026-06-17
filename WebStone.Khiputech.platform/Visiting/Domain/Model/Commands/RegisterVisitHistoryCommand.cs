namespace WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;

public record RegisterVisitHistoryCommand(
    int ArtworkId,
    string SessionId
);