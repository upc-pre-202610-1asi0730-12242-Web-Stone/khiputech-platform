using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Domain.Repositories;

public interface IAlertRepository
{
    Task<Alert?> FindByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<Alert>> GetActiveAlertsAsync(CancellationToken ct);
    Task AddAsync(Alert alert, CancellationToken ct);
    void Update(Alert alert);
}