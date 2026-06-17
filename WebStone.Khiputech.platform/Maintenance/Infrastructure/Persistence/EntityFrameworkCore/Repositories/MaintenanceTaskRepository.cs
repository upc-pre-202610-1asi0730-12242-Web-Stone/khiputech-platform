using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Maintenance.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Maintenance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MaintenanceTaskRepository(AppDbContext context) : BaseRepository<MaintenanceTask>(context), IMaintenanceTaskRepository
{
    public async Task<MaintenanceTask?> FindByIdAsync(int id, CancellationToken ct)
        => await Context.Set<MaintenanceTask>().FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<IEnumerable<MaintenanceTask>> GetTasksAsync(string? status = null, CancellationToken ct = default)
    {
        var query = Context.Set<MaintenanceTask>().AsQueryable();
        if (!string.IsNullOrEmpty(status))
            query = query.Where(t => t.Status == status);
        return await query.OrderByDescending(t => t.ScheduledAt).ToListAsync(ct);
    }

    public async Task AddAsync(MaintenanceTask task, CancellationToken ct)
        => await Context.Set<MaintenanceTask>().AddAsync(task, ct);
}