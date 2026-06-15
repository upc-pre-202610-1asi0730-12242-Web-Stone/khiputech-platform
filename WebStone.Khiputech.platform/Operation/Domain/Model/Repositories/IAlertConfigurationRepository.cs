using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Domain.Repositories;

public interface IAlertConfigurationRepository
{
    Task<AlertConfiguration?> GetAsync(CancellationToken ct);
    void Update(AlertConfiguration config);
}