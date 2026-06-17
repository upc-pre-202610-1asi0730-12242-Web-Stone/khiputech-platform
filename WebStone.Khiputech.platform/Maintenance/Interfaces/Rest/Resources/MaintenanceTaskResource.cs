namespace WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

public record MaintenanceTaskResource(
    int Id,
    int ArtworkId,
    string ArtworkName,
    DateTime StartDate,
    DateTime EndDate,
    string Reason,
    string Status,
    bool IsOverdue
);