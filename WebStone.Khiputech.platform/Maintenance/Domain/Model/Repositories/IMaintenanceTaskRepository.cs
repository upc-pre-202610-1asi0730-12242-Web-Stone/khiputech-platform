using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Maintenance.Domain.Repositories;

public interface IMaintenanceTaskRepository
{
    Task<MaintenanceTask?> FindByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<MaintenanceTask>> GetTasksAsync(string? status = null, CancellationToken ct = default);
    Task AddAsync(MaintenanceTask task, CancellationToken ct);
    void Update(MaintenanceTask task);
}