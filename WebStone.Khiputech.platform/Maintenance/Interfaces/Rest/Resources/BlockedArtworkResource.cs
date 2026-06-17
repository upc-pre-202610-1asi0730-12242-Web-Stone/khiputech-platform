namespace WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

public record BlockedArtworkResource(
    int ArtworkId,
    string ArtworkName,
    string Reason,
    DateTime EstimatedEndDate
);