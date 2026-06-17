namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;

public record ArtworkResource(
    int Id,
    string Code,
    string Title,
    string Artist,
    string Description,
    string ImageUrl,
    bool IsActive
);