namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;

public record AddFavoriteResource(
    int ArtworkId,
    string SessionId
);