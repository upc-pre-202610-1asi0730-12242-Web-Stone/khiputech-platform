using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Capacity.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Capacity.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SensorRepository(AppDbContext context) : BaseRepository<Sensor>(context), ISensorRepository
{
    public async Task<IEnumerable<Sensor>> ListAsync(CancellationToken ct)
        => await Context.Set<Sensor>().ToListAsync(ct);
}