using WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Analytics.Domain.Repositories;

public interface IArtworkStatRepository
{
    Task<IEnumerable<ArtworkStat>> GetTopArtworksAsync(int limit, CancellationToken ct);
    Task<IEnumerable<ArtworkStat>> GetRankingAsync(string period, CancellationToken ct);
    Task<ArtworkStat?> GetByIdAsync(int id, CancellationToken ct);
}