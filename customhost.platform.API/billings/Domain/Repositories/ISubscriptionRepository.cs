using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.billings.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    /// <summary>
    /// Find if it is a unique active subscription for a hotel
    /// </summary>
    
    Task<bool> ExistsByHotelIdAndSubscriptionPlanIdAndActive(int hotelId, int SubscriptionPlanId);
    
    
}