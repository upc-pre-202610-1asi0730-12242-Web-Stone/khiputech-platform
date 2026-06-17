namespace WebStone.Khiputech.Platform.Maintenance.Domain.Model.Commands;

public record ScheduleMaintenanceCommand(int ArtworkId, string ArtworkName, string Reason, string ScheduledBy);