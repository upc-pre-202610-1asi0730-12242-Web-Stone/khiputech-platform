using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
/// Extension methods for ModelBuilder to configure IAM entities.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies IAM entity configurations to the model builder.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.ToTable("users"); 
        
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            entity.HasIndex(u => u.Username)
                .IsUnique();

            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Username");

            entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash");

            entity.Property(u => u.Type)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Type")
                .HasDefaultValue("public");

            entity.Property(u => u.Permissions)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Permissions");

            entity.Property(u => u.CreatedAt)
                .HasColumnName("CreatedAt");

            entity.Property(u => u.UpdatedAt)
                .HasColumnName("UpdatedAt");
        });
    }
}