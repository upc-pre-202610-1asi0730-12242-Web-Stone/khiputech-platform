namespace WebStone.Khiputech.Platform.Maintenance.Domain.Model.Commands;

public record ScheduleMaintenanceCommand(
    int ArtworkId,
    string ArtworkName,
    DateTime StartDate,
    DateTime EndDate,
    string Reason
);