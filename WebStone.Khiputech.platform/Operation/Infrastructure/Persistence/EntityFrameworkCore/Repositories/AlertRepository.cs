using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<Alert?> FindByIdAsync(int id, CancellationToken ct)
        => await Context.Set<Alert>().FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IEnumerable<Alert>> GetActiveAlertsAsync(CancellationToken ct)
        => await Context.Set<Alert>().Where(a => a.Status == "active").OrderByDescending(a => a.CreatedAt).ToListAsync(ct);

    public async Task AddAsync(Alert alert, CancellationToken ct)
        => await Context.Set<Alert>().AddAsync(alert, ct);
}