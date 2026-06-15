using Microsoft.AspNetCore.Mvc;
using WebStone.Khiputech.Platform.Visiting.Application.QueryServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Transform;

namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest;

[ApiController]
[Route("api/artifacts")]
public class ArtworkController(IArtworkQueryService artworkQueryService)
    : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetArtworkById(int id)
    {
        var query = new GetArtworkByIdQuery(id);
        var artwork = await artworkQueryService.Handle(query);

        if (artwork is null)
            return NotFound(new
            {
                message = $"Artwork with id {id} was not found."
            });

        var resource =
            ArtworkResourceFromEntityAssembler
                .ToResourceFromEntity(artwork);

        return Ok(resource);
    }
}