using WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Visiting.Interfaces.Rest.Transform;

public static class AddFavoriteCommandFromResourceAssembler
{
    public static AddFavoriteCommand ToCommandFromResource(AddFavoriteResource resource)
    {
        return new AddFavoriteCommand(
            resource.ArtworkId,
            resource.SessionId
        );
    }
}