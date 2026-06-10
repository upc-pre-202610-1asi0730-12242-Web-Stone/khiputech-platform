using WebStone.Khiputech.Platform.Visiting.Application.QueryServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Application.Internal.QueryServices;

public class ArtworkQueryService(IArtworkRepository artworkRepository) : IArtworkQueryService
{
    public async Task<Artwork?> Handle(GetArtworkByIdQuery query)
    {
        return await artworkRepository.FindByIdAsync(query.Id);
    }
}