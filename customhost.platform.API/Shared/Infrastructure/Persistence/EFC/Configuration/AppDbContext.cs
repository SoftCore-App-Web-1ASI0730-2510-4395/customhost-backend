using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost_backend.crm.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost_backend.Shared.Infrastructure.Interfaces.Persistence.EFC.Configuration.Extensions;
using customhost.platform.API.billings.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost.platform.API.profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{    // CRM DbSets
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<StaffMember> StaffMembers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    // IAM DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Roles { get; set; }
    
    // Guest Experience DbSets
    public DbSet<DeviceModel> DeviceModels { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DevicePreference> DevicePreferences { get; set; }
    public DbSet<UserDevicePreference> UserDevicePreferences { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        
        builder.ApplyCrmConfiguration();
        builder.ApplyGuestExperienceConfiguration();
        builder.ApplyProfileConfiguration();
        
        builder.ApplyIamConfiguration();
        builder.ApplyBillingsConfiguration();

        builder.UseSnakeCaseNamingConvention();
        
        
    }
}