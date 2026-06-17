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
        var tasks = await taskRepository.GetTasksAsync(query.Status, ct);
        return tasks.Select(MaintenanceTaskResourceFromEntityAssembler.ToResourceFromEntity);
    }

    public async Task<IEnumerable<int>> Handle(GetBlockedArtworksQuery query, CancellationToken ct)
    {
        var activeTasks = await taskRepository.GetTasksAsync("pending", ct);
        var inProgressTasks = await taskRepository.GetTasksAsync("in_progress", ct);
        var blockedArtworkIds = activeTasks.Concat(inProgressTasks).Select(t => t.ArtworkId).Distinct();
        return blockedArtworkIds;
    }
}