using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.billings.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<bool> ExistsByHotelIdAndSubscriptionPlanIdAndActive(int hotelId, int subscriptionPlanId)
    {
        
        return await context.Set<Subscription>()
            .AnyAsync(s => s.HotelId == hotelId 
                           && s.SubscriptionPlanId == subscriptionPlanId 
                           && s.Status == "Active");
    }
}