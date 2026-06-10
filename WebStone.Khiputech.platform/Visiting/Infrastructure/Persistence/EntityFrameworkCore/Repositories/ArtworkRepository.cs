using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ArtworkRepository(DbContext context) : IArtworkRepository
{
    public async Task<Artwork?> FindByIdAsync(int id)
    {
        return await context.Set<Artwork>().FindAsync(id);
    }
}