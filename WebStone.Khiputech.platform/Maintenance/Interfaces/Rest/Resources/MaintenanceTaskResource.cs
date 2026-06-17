namespace WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

public record MaintenanceTaskResource(
    int Id,
    int ArtworkId,
    string ArtworkName,
    string Reason,
    string Status,
    string? ScheduledBy,
    DateTime ScheduledAt
);