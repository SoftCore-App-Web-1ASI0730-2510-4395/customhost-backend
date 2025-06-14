using customhost_backend.crm.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost_backend.Shared.Infrastructure.Interfaces.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
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

        builder.UseSnakeCaseNamingConvention();
    }
}