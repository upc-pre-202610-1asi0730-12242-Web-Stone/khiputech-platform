using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Capacity.Domain.Repositories;

public interface ISensorRepository
{
    Task<IEnumerable<Sensor>> ListAsync(CancellationToken ct);
}