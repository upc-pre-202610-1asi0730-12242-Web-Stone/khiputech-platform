using WebStone.Khiputech.Platform.Operation.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Operation.Application.QueryServices;

public interface IOperationQueryService
{
    Task<IEnumerable<AlertResource>> Handle(GetActiveAlertsQuery query, CancellationToken ct);
    Task<AlertConfigurationResource> Handle(GetAlertConfigurationQuery query, CancellationToken ct);
}