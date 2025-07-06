using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Room Device entity configuration for Entity Framework Core
/// </summary>
public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("room_devices");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.RoomId)
               .HasColumnName("room_id")
               .IsRequired();
               
        builder.Property(e => e.DeviceModelId)
               .HasColumnName("iot_device_id")
               .IsRequired();
               
        builder.Property(e => e.Status)
               .HasColumnName("status")
               .HasMaxLength(20)
               .IsRequired();
               
        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at");
        
        // Relationship with DeviceModel
        builder.HasOne(e => e.DeviceModel)
               .WithMany()
               .HasForeignKey(e => e.DeviceModelId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
