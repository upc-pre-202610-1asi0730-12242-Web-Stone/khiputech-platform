using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class OperationRecommendationConfiguration 
    : IEntityTypeConfiguration<OperationRecommendation>
{
    public void Configure(EntityTypeBuilder<OperationRecommendation> builder)
    {
        builder.ToTable("operation_recommendations");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .IsRequired();
        builder.Property(r => r.RoomName)
            .HasColumnName("room_name")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(r => r.Issue)
            .HasColumnName("issue")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(r => r.SuggestedAction)
            .HasColumnName("suggested_action")
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(r => r.GeneratedAt)
            .HasColumnName("generated_at")
            .IsRequired();
    }
}