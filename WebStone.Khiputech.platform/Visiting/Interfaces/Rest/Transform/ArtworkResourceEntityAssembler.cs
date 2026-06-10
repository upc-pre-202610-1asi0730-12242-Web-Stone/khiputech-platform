using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Transform;

public static class ArtworkResourceFromEntityAssembler
{
    public static ArtworkResource ToResourceFromEntity(Artwork artwork)
    {
        return new ArtworkResource(
            artwork.Id,
            artwork.Code,
            artwork.Title,
            artwork.Artist,
            artwork.Description,
            artwork.ImageUrl,
            artwork.IsActive
        );
    }
}