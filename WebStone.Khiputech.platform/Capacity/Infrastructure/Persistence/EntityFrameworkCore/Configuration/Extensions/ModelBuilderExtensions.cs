using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Capacity.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyCapacityConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Room>(entity =>
        {
            entity.ToTable("rooms");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).ValueGeneratedOnAdd();
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Capacity).IsRequired();
            entity.Property(r => r.CurrentOccupancy).IsRequired();
            entity.Property(r => r.Status).IsRequired().HasMaxLength(20);
            entity.Property(r => r.Description).HasMaxLength(500);
            entity.Property(r => r.CreatedAt);
            entity.Property(r => r.UpdatedAt);
        });

        builder.Entity<Sensor>(entity =>
        {
            entity.ToTable("sensors");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).ValueGeneratedOnAdd();
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Location).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Type).IsRequired().HasMaxLength(50);
            entity.Property(s => s.Status).IsRequired().HasMaxLength(20);
            entity.Property(s => s.CreatedAt);
            entity.Property(s => s.UpdatedAt);
        });
    }
}