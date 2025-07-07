using customhost_backend.billings.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace customhost.platform.API.billings.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyBillingsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Subscription>().HasKey(s => s.Id);
        builder.Entity<Subscription>().Property(s=> s.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<SubscriptionPlan>().HasKey(sp => sp.Id);
        builder.Entity<SubscriptionPlan>().Property(sp=> sp.Id).IsRequired().ValueGeneratedOnAdd();
    }
}