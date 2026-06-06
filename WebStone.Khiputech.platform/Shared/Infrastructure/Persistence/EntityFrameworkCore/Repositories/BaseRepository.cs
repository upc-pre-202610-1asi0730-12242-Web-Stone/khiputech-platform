using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public abstract class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly AppDbContext Context = context;

    public async Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().FindAsync([id], cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);

    public void Update(TEntity entity)
        => Context.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity)
        => Context.Set<TEntity>().Remove(entity);

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);
}