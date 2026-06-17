using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;

namespace WebStone.Khiputech.Platform.Visiting.Application.QueryServices;

public interface IFavoriteQueryService
{
    Task<IEnumerable<Favorite>> Handle(GetFavoritesQuery query);
}