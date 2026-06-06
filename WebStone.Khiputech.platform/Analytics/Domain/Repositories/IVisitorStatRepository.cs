using WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Analytics.Domain.Repositories;

public interface IVisitorStatRepository
{
    Task<VisitorStat?> GetByDateAsync(DateOnly date, CancellationToken ct);
    Task<int[]> GetVisitsByHourAsync(DateOnly date, CancellationToken ct);
}