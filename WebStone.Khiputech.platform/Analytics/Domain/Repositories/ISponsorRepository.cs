using WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Analytics.Domain.Repositories;

public interface ISponsorRepository
{
    Task<IEnumerable<Sponsor>> GetAllAsync(CancellationToken ct);
    Task<Sponsor?> GetByIdAsync(int id, CancellationToken ct);
}