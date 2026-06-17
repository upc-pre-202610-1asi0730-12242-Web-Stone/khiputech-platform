using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply IAM configuration
        modelBuilder.ApplyIamConfiguration();
        
        // Other bounded context configurations will be added here later
        // modelBuilder.ApplyCapacityConfiguration();
        // modelBuilder.ApplyOperationConfiguration();
        // modelBuilder.ApplyMaintenanceConfiguration();
        // modelBuilder.ApplyAnalyticsConfiguration();
    }
}