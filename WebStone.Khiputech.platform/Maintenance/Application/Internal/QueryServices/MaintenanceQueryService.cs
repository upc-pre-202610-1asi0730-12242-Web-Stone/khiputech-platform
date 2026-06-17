using WebStone.Khiputech.Platform.Maintenance.Application.QueryServices;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Maintenance.Domain.Repositories;
using WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Resources;
using WebStone.Khiputech.Platform.Maintenance.Interfaces.Rest.Transform;

namespace WebStone.Khiputech.Platform.Maintenance.Application.Internal.QueryServices;

public class MaintenanceQueryService(IMaintenanceTaskRepository taskRepository) : IMaintenanceQueryService
{
    public async Task<IEnumerable<MaintenanceTaskResource>> Handle(GetMaintenanceTasksQuery query, CancellationToken ct)
    {
        var tasks = await taskRepository.ListAsync(query.ActiveOnly, ct);
        return tasks.Select(MaintenanceTaskResourceFromEntityAssembler.ToResourceFromEntity);
    }

    public async Task<MaintenanceTaskResource?> Handle(GetMaintenanceTaskByIdQuery query, CancellationToken ct)
    {
        var task = await taskRepository.FindByIdAsync(query.Id, ct);
        return task != null ? MaintenanceTaskResourceFromEntityAssembler.ToResourceFromEntity(task) : null;
    }

    public async Task<IEnumerable<BlockedArtworkResource>> Handle(GetBlockedArtworksQuery query, CancellationToken ct)
    {
        var activeTasks = await taskRepository.ListAsync(true, ct);
        return activeTasks.Select(t => new BlockedArtworkResource(
            t.ArtworkId,
            t.ArtworkName,
            t.Reason,
            t.EndDate
        ));
    }
}

