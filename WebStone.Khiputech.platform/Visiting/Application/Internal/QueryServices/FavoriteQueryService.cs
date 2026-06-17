using WebStone.Khiputech.Platform.Visiting.Application.QueryServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Application.Internal.QueryServices;

public class FavoriteQueryService(IFavoriteRepository favoriteRepository) : IFavoriteQueryService
{
    public async Task<IEnumerable<Favorite>> Handle(GetFavoritesQuery query)
    {
        return await favoriteRepository.FindBySessionIdAsync(query.SessionId);
    }
}