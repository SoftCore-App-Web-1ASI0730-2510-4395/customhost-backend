using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Interfaces.REST.Resources;

namespace customhost_backend.billings.Interfaces.REST.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {

        return new CreateSubscriptionCommand(
            resource.HotelId,
            resource.SubscriptionPlanId,
            resource.Status,
            resource.StartDate,
            resource.EndDate
            
        );
    }
}