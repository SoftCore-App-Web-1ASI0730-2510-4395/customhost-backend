using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context
        
        // Rol Configuration
        builder.Entity<Rol>().HasKey(r => r.Id);
        builder.Entity<Rol>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Rol>().Property(r => r.RoleName)
            .IsRequired()
            .HasConversion<string>();
        
        // User Configuration
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.RolId).IsRequired();
        
        // User-Rol Relationship
        builder.Entity<User>()
            .HasOne(u => u.Rol)
            .WithMany()
            .HasForeignKey(u => u.RolId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Seed default roles
        builder.Entity<Rol>().HasData(
            new { Id = 1, RoleName = ERoles.GUEST },
            new { Id = 2, RoleName = ERoles.STAFF },
            new { Id = 3, RoleName = ERoles.ADMIN }
        );
    }
}