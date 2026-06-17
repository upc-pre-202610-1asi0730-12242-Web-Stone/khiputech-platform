using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Transform;

public static class FavoriteResourceFromEntityAssembler
{
    public static FavoriteResource ToResourceFromEntity(Favorite favorite)
    {
        return new FavoriteResource(
            favorite.Id,
            favorite.ArtworkId,
            favorite.SessionId,
            favorite.CreatedAt
        );
    }
}