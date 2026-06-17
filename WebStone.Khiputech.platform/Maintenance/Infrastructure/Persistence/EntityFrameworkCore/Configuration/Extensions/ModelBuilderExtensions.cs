using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Maintenance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMaintenanceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<MaintenanceTask>(entity =>
        {
            entity.ToTable("maintenance_tasks");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.ArtworkId).IsRequired();
            entity.Property(t => t.ArtworkName).IsRequired().HasMaxLength(200);
            entity.Property(t => t.StartDate).IsRequired();
            entity.Property(t => t.EndDate).IsRequired();
            entity.Property(t => t.Reason).IsRequired().HasMaxLength(500);
            entity.Property(t => t.Status).IsRequired().HasMaxLength(20);
            entity.Property(t => t.CreatedAt);
            entity.Property(t => t.UpdatedAt);

            // Índices para consultas frecuentes
            entity.HasIndex(t => t.ArtworkId);
            entity.HasIndex(t => t.Status);
            entity.HasIndex(t => t.StartDate);
        });
    }
}