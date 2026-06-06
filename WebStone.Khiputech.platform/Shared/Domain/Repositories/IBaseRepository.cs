using System.Linq.Expressions;

namespace WebStone.Khiputech.Platform.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    void Update(TEntity entity);
    
    void Remove(TEntity entity);
    
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}