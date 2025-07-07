using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Room Device Preference entity configuration for Entity Framework Core
/// </summary>
public class DevicePreferenceConfiguration : IEntityTypeConfiguration<DevicePreference>
{
    public void Configure(EntityTypeBuilder<DevicePreference> builder)
    {
        builder.ToTable("room_device_preferences");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.DeviceId)
               .HasColumnName("room_device_id")
               .IsRequired();
               
        builder.Property(e => e.Preferences)
               .HasColumnName("preferences")
               .HasColumnType("text")
               .IsRequired();
               
        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at");
        
        // Relationship with Device
        builder.HasOne(e => e.Device)
               .WithMany()
               .HasForeignKey(e => e.DeviceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
