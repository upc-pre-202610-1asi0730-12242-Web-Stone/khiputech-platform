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

    public async Task<IEnumerable<MaintenanceTask>> ListAsync(bool? activeOnly, CancellationToken ct)
    {
        var query = Context.Set<MaintenanceTask>().AsQueryable();
        if (activeOnly == true)
            query = query.Where(t => t.Status == "pending" || t.Status == "in_progress");
        else if (activeOnly == false)
            query = query.Where(t => t.Status == "completed" || t.Status == "cancelled");
        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync(ct);
    }

    public async Task<IEnumerable<int>> GetBlockedArtworkIdsAsync(CancellationToken ct)
    {
        var activeTasks = await ListAsync(true, ct);
        return activeTasks.Select(t => t.ArtworkId);
    }

    public async Task AddAsync(MaintenanceTask task, CancellationToken ct)
        => await Context.Set<MaintenanceTask>().AddAsync(task, ct);
}