using WebStone.Khiputech.Platform.Visiting.Application.QueryServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Application.Internal.QueryServices;

public class VisitHistoryQueryService(IVisitHistoryRepository visitHistoryRepository)
    : IVisitHistoryQueryService
{
    public async Task<IEnumerable<VisitHistory>> Handle(GetVisitHistoryQuery query)
    {
        return await visitHistoryRepository.FindBySessionIdAsync(query.SessionId);
    }
}