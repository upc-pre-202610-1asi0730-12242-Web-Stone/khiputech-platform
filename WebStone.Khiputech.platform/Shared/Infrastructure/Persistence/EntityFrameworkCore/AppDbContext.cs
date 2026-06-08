using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
namespace WebStone.Khiputech.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<OperationRecommendation> OperationRecommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<OperationRecommendation>().HasKey(r => r.Id);
        builder.Entity<OperationRecommendation>().Property(r => r.RoomName).IsRequired();
        builder.Entity<OperationRecommendation>().Property(r=>r.Issue).IsRequired();
        builder.Entity<OperationRecommendation>().Property(r=>r.SuggestedAction).IsRequired();
        builder.Entity<OperationRecommendation>().Property(r=>r.GeneratedAt).IsRequired();
    }
}