using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

public interface IArtworkRepository
{
    Task<Artwork?> FindByIdAsync(int id);
}