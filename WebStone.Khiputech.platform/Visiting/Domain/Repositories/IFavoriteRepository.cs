using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

public interface IFavoriteRepository
{
    Task<IEnumerable<Favorite>> FindBySessionIdAsync(string sessionId);

    Task<Favorite?> FindByIdAsync(int id);

    Task AddAsync(Favorite favorite);

    void Delete(Favorite favorite);
}