using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Capacity.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Capacity.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RoomRepository(AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
    public async Task<Room?> FindByIdAsync(int id, CancellationToken ct)
        => await Context.Set<Room>().FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IEnumerable<Room>> ListAsync(CancellationToken ct)
        => await Context.Set<Room>().ToListAsync(ct);

    public Task UpdateAsync(Room room, CancellationToken ct)
    {
        Context.Set<Room>().Update(room);
        return Task.CompletedTask;
    }

    public void Update(Room room) => Context.Set<Room>().Update(room);
}