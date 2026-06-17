using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<OperationRecommendation> OperationRecommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyIamConfiguration();
        modelBuilder.ApplyOperationConfiguration();
    }
}