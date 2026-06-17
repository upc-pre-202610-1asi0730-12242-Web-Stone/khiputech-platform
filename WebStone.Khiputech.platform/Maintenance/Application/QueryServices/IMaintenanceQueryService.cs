using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Maintenance.Application.QueryServices;

public interface IMaintenanceQueryService
{
    Task<IEnumerable<MaintenanceTaskResource>> Handle(GetMaintenanceTasksQuery query, CancellationToken ct);
    Task<MaintenanceTaskResource?> Handle(GetMaintenanceTaskByIdQuery query, CancellationToken ct);
    Task<IEnumerable<BlockedArtworkResource>> Handle(GetBlockedArtworksQuery query, CancellationToken ct);
}