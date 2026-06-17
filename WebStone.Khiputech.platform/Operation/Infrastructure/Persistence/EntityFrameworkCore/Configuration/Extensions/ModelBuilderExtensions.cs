using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOperationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>(entity =>
        {
            entity.ToTable("alerts");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedOnAdd();
            entity.Property(a => a.RoomName).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Type).IsRequired().HasMaxLength(20);
            entity.Property(a => a.Message).IsRequired().HasMaxLength(500);
            entity.Property(a => a.Status).IsRequired().HasMaxLength(20);
            entity.Property(a => a.TriggeredBy).HasMaxLength(100);
            entity.Property(a => a.CreatedAt);
            entity.Property(a => a.ResolvedAt);
        });

        builder.Entity<AlertConfiguration>(entity =>
        {
            entity.ToTable("alert_configurations");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.ModerateThreshold);
            entity.Property(c => c.CriticalThreshold);
            entity.Property(c => c.NotifyEmail);
            entity.Property(c => c.NotifyWhatsApp);
            entity.Property(c => c.NotifySms);
            entity.Property(c => c.NotifyPanel);
            entity.Property(c => c.ContactCivilDefense).HasMaxLength(200);
            entity.Property(c => c.UpdatedAt);
        });
    }
}