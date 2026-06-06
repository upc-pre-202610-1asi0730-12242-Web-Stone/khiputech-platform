using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebStone.Khiputech.Platform.Analytics.Application.QueryServices;
using WebStone.Khiputech.Platform.Analytics.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Analytics.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Analytics endpoints for curator and manager")]
public class AnalyticsController(IAnalyticsQueryService analyticsQueryService) : ControllerBase
{
    [HttpGet("stats")]
    [SwaggerOperation(Summary = "Get main dashboard statistics")]
    public async Task<ActionResult<StatsResource>> GetStats([FromQuery] DateOnly? date, CancellationToken ct)
    {
        var query = new GetStatsQuery(date ?? DateOnly.FromDateTime(DateTime.Today));
        var result = await analyticsQueryService.Handle(query, ct);
        return Ok(result);
    }

    [HttpGet("top-artworks")]
    [SwaggerOperation(Summary = "Get most visited artworks")]
    public async Task<ActionResult<IEnumerable<TopArtworkResource>>> GetTopArtworks([FromQuery] int limit = 5, CancellationToken ct = default)
    {
        var query = new GetTopArtworksQuery(limit);
        var result = await analyticsQueryService.Handle(query, ct);
        return Ok(result);
    }

    [HttpGet("visits-by-hour")]
    [SwaggerOperation(Summary = "Get visits distribution per hour for a given date")]
    public async Task<ActionResult<int[]>> GetVisitsByHour([FromQuery] DateOnly? date, CancellationToken ct)
    {
        var query = new GetVisitsByHourQuery(date ?? DateOnly.FromDateTime(DateTime.Today));
        var result = await analyticsQueryService.Handle(query, ct);
        return Ok(result);
    }

    [HttpGet("ranking")]
    [SwaggerOperation(Summary = "Get artwork ranking by engagement for a period (today, week, month)")]
    public async Task<ActionResult<IEnumerable<RankingArtworkResource>>> GetRanking([FromQuery] string period = "week", CancellationToken ct = default)
    {
        var query = new GetRankingQuery(period);
        var result = await analyticsQueryService.Handle(query, ct);
        return Ok(result);
    }

    [HttpGet("sponsors")]
    [SwaggerOperation(Summary = "Get all sponsors")]
    public async Task<ActionResult<IEnumerable<SponsorResource>>> GetSponsors(CancellationToken ct)
    {
        var query = new GetSponsorsQuery();
        var result = await analyticsQueryService.Handle(query, ct);
        return Ok(result);
    }
}