using customhost_backend.profiles.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace customhost.platform.API.profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfileConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(p => p.HotelId).IsRequired(false);

        
    }
}