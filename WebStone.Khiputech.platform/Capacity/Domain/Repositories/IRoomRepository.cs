using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Capacity.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room?> FindByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<Room>> ListAsync(CancellationToken ct);
    Task UpdateAsync(Room room, CancellationToken ct);
    void Update(Room room);
}