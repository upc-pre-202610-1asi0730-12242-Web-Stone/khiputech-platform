namespace WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

public record ScheduleMaintenanceRequest(
    int ArtworkId,
    string ArtworkName,
    DateTime StartDate,
    DateTime EndDate,
    string Reason
);