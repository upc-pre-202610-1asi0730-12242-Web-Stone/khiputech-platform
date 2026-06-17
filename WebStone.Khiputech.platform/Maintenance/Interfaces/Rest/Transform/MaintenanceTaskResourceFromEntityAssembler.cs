using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Transform;

public static class MaintenanceTaskResourceFromEntityAssembler
{
    public static MaintenanceTaskResource ToResourceFromEntity(MaintenanceTask task)
    {
        return new MaintenanceTaskResource(
            task.Id,
            task.ArtworkId,
            task.ArtworkName,
            task.StartDate,
            task.EndDate,
            task.Reason,
            task.Status,
            task.IsOverdue
        );
    }
}
