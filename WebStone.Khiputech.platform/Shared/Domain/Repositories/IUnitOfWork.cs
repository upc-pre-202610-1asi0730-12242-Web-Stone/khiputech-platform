namespace WebStone.Khiputech.Platform.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}