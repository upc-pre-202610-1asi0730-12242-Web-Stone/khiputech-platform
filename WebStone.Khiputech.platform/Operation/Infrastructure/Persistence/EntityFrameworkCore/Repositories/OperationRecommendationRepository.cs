using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class OperationRecommendationRepository(AppDbContext context) 
    : BaseRepository<OperationRecommendation>(context), IOperationRecommendationRepository
{
    public async Task<IEnumerable<OperationRecommendation>> GetAllAsync(CancellationToken ct)
        => await Context.Set<OperationRecommendation>()
            .OrderByDescending(r => r.GeneratedAt)
            .ToListAsync(ct);
    
    public async Task AddAsync(OperationRecommendation recommendation, CancellationToken ct)
        => await Context.Set<OperationRecommendation>().AddAsync(recommendation, ct);
}