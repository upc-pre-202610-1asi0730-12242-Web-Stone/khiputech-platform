using Microsoft.AspNetCore.Mvc;
using WebStone.Khiputech.Platform.Visiting.Application.CommandServices;
using WebStone.Khiputech.Platform.Visiting.Application.QueryServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;
using WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Transform;

namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest;

[ApiController]
[Route("api/favorites")]
public class FavoriteController(
    IFavoriteCommandService favoriteCommandService,
    IFavoriteQueryService favoriteQueryService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetFavorites([FromQuery] string sessionId)
    {
        var query = new GetFavoritesQuery(sessionId);
        var favorites = await favoriteQueryService.Handle(query);

        var resources = favorites
            .Select(FavoriteResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteResource resource)
    {
        var command =
            AddFavoriteCommandFromResourceAssembler.ToCommandFromResource(resource);

        await favoriteCommandService.Handle(command);

        return Ok(new { message = "Favorite added successfully." });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFavorite(int id)
    {
        var command = new DeleteFavoriteCommand(id);

        await favoriteCommandService.Handle(command);

        return Ok(new { message = "Favorite deleted successfully." });
    }
}