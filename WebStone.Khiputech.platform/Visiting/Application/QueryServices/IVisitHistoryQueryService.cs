using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Queries;

namespace WebStone.Khiputech.Platform.Visiting.Application.QueryServices;

public interface IVisitHistoryQueryService
{
    Task<IEnumerable<VisitHistory>> Handle(GetVisitHistoryQuery query);
}