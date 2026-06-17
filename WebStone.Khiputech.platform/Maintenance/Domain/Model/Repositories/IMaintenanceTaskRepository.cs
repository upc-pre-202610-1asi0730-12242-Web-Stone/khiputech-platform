using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Maintenance.Domain.Repositories;

public interface IMaintenanceTaskRepository
{
    Task<MaintenanceTask?> FindByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<MaintenanceTask>> ListAsync(bool? activeOnly, CancellationToken ct);
    Task<IEnumerable<int>> GetBlockedArtworkIdsAsync(CancellationToken ct);
    Task AddAsync(MaintenanceTask task, CancellationToken ct);
    void Update(MaintenanceTask task);
}