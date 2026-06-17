using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply IAM configuration
        modelBuilder.ApplyIamConfiguration();

        // Apply Visiting configuration
        modelBuilder.Entity<Artwork>().ToTable("artworks");
        modelBuilder.Entity<Favorite>().ToTable("favorites");
        
        // Other bounded context configurations will be added here later
        // modelBuilder.ApplyCapacityConfiguration();
        // modelBuilder.ApplyOperationConfiguration();
        // modelBuilder.ApplyMaintenanceConfiguration();
        // modelBuilder.ApplyAnalyticsConfiguration();
    }
}