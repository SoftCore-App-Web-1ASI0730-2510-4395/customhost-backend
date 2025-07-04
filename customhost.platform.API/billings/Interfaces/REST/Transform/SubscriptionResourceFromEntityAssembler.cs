using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Interfaces.REST.Resources;

namespace customhost_backend.billings.Interfaces.REST.Transform;

public class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        return new SubscriptionResource(
            entity.Id,
            entity.HotelId,
            entity.SubscriptionPlanId,
            entity.Status,
            entity.StartDate,
            entity.EndDate
        );
    }
}