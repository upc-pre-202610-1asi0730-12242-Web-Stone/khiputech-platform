using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class FavoriteRepository(AppDbContext context) : IFavoriteRepository
{
    public async Task<IEnumerable<Favorite>> FindBySessionIdAsync(string sessionId)
    {
        return await context.Set<Favorite>()
            .Where(favorite => favorite.SessionId == sessionId)
            .ToListAsync();
    }

    public async Task<Favorite?> FindByIdAsync(int id)
    {
        return await context.Set<Favorite>()
            .FindAsync(id);
    }

    public async Task AddAsync(Favorite favorite)
    {
        await context.Set<Favorite>().AddAsync(favorite);
        await context.SaveChangesAsync();
    }

    public void Delete(Favorite favorite)
    {
        context.Set<Favorite>().Remove(favorite);
        context.SaveChanges();
    }
}