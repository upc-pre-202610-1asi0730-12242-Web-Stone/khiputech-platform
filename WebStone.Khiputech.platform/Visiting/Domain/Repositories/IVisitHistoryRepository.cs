using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

public interface IVisitHistoryRepository
{
    Task<IEnumerable<VisitHistory>> FindBySessionIdAsync(string sessionId);

    Task AddAsync(VisitHistory visitHistory);
}