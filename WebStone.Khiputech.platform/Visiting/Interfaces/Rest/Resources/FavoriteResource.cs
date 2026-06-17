namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;

public record FavoriteResource(
    int Id,
    int ArtworkId,
    string SessionId,
    DateTime CreatedAt
);