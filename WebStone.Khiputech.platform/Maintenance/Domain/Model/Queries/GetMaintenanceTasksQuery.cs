namespace WebStone.Khiputech.Platform.Maintenance.Domain.Model.Queries;

public record GetMaintenanceTasksQuery(string? Status = null); // pending, in_progress, completed