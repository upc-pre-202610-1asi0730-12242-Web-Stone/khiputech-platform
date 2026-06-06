using WebStone.Khiputech.Platform.Analytics.Application.QueryServices;
using WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Analytics.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Analytics.Domain.Repositories;
using WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Analytics.Application.Internal.QueryServices;

public class AnalyticsQueryService : IAnalyticsQueryService
{
    // Luego inyectaremos repositorios reales.
    // private readonly IArtworkStatRepository _artworkStatRepo;
    // private readonly IVisitorStatRepository _visitorStatRepo;
    // private readonly ISponsorRepository _sponsorRepo;

    public AnalyticsQueryService(/* inyectar repositorios */)
    {
    }

    public async Task<StatsResource> Handle(GetStatsQuery query, CancellationToken ct)
    {
        // Simulación: reemplazar con datos reales desde repositorios
        await Task.Delay(10, ct);
        return new StatsResource(
            VisitantesHoy: 284,
            VisitantesChange: 12,
            ObrasEscaneadas: 1247,
            ObrasChange: 8,
            DuracionMedia: 47,
            DuracionChange: 5,
            Nps: 72,
            NpsChange: 4
        );
    }

    public async Task<IEnumerable<TopArtworkResource>> Handle(GetTopArtworksQuery query, CancellationToken ct)
    {
        await Task.Delay(10, ct);
        return new List<TopArtworkResource>
        {
            new(1, "La persistencia de la memoria", "Sala 2", 342, 78, "abierto"),
            new(2, "La noche estrellada", "Sala 1", 298, 65, "abierto"),
            new(3, "El grito", "Sala 1", 256, 54, "mantenimiento"),
            new(4, "Guernica", "Sala 3", 187, 82, "abierto"),
            new(5, "El beso", "Sala 2", 165, 71, "abierto")
        }.Take(query.Limit);
    }

    public async Task<int[]> Handle(GetVisitsByHourQuery query, CancellationToken ct)
    {
        await Task.Delay(10, ct);
        return new int[] { 12,8,5,7,15,28,45,62,58,42,35,48,72,85,68,54,38,25,18,12,8,6,4,3 };
    }

    public async Task<IEnumerable<RankingArtworkResource>> Handle(GetRankingQuery query, CancellationToken ct)
    {
        await Task.Delay(10, ct);
        var data = new List<RankingArtworkResource>
        {
            new(1, "La persistencia de la memoria", "Sala 2", "Salvador Dalí", 98, 4.2, "up"),
            new(2, "La noche estrellada", "Sala 1", "Van Gogh", 92, 3.8, "up"),
            new(3, "El beso", "Sala 2", "Gustav Klimt", 87, 3.5, "down"),
            new(4, "Guernica", "Sala 3", "Picasso", 81, 4.5, "down"),
            new(5, "El pensador", "Sala 3", "Rodin", 76, 3.2, "up")
        };
        // Filtrar por periodo si es necesario
        return data;
    }

    public async Task<IEnumerable<SponsorResource>> Handle(GetSponsorsQuery query, CancellationToken ct)
    {
        await Task.Delay(10, ct);
        return new List<SponsorResource>
        {
            new(1, "Coca-Cola", "Sala 1 - Arte Moderno", 3420, 8500, "activo"),
            new(2, "Banco Interbank", "Sala 2 - Contemporáneo", 2890, 7200, "activo"),
            new(3, "Movistar", "Sala 3 - Clásico", 2150, 5400, "activo"),
            new(4, "Backus", "Cafetería", 1890, 4200, "inactivo"),
            new(5, "Samsung", "Entrada Principal", 3120, 7800, "activo")
        };
    }
}