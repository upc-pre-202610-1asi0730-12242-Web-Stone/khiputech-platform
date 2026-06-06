using WebStone.Khiputech.Platform.Analytics.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources; // necesitaremos luego los DTOs

namespace WebStone.Khiputech.Platform.Analytics.Application.QueryServices;

public interface IAnalyticsQueryService
{
    Task<StatsResource> Handle(GetStatsQuery query, CancellationToken ct);
    Task<IEnumerable<TopArtworkResource>> Handle(GetTopArtworksQuery query, CancellationToken ct);
    Task<int[]> Handle(GetVisitsByHourQuery query, CancellationToken ct);
    Task<IEnumerable<RankingArtworkResource>> Handle(GetRankingQuery query, CancellationToken ct);
    Task<IEnumerable<SponsorResource>> Handle(GetSponsorsQuery query, CancellationToken ct);
}