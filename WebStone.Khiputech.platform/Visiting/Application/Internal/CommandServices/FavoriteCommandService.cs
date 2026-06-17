using WebStone.Khiputech.Platform.Visiting.Application.CommandServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Application.Internal.CommandServices;

public class FavoriteCommandService(IFavoriteRepository favoriteRepository) : IFavoriteCommandService
{
    public async Task Handle(AddFavoriteCommand command)
    {
        var favorite = new Favorite
        {
            ArtworkId = command.ArtworkId,
            SessionId = command.SessionId
        };

        await favoriteRepository.AddAsync(favorite);
    }

    public async Task Handle(DeleteFavoriteCommand command)
    {
        var favorite = await favoriteRepository.FindByIdAsync(command.Id);

        if (favorite is null)
            return;

        favoriteRepository.Delete(favorite);
    }
}