using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RecommendationRepository : IRecommendationRepository
{
    private readonly AppDbContext _context;

    public RecommendationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OperationRecommendation>> GetAllAsync()
    {
        return await _context.OperationRecommendations.ToListAsync();
    }
}