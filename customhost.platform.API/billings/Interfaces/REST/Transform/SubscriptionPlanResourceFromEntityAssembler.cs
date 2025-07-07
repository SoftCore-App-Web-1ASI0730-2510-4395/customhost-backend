using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Interfaces.REST.Resources;

namespace customhost_backend.billings.Interfaces.REST.Transform;

public static class SubscriptionPlanResourceFromEntityAssembler
{
    public static SubscriptionPlanResource ToResourceFromEntity(SubscriptionPlan entity)
    {
        return new SubscriptionPlanResource(
            entity.Id,
            entity.Name,
            entity.MaxRooms,
            entity.MaxStaffMembers,
            entity.MaxDevices,
            entity.Price,
            entity.Currency
        );
    }
}