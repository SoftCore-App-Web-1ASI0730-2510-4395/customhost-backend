using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Interfaces.REST.Resources;
namespace customhost_backend.billings.Interfaces.REST.Transform;

public static class CreateSubscriptionPlanCommandFromResourceAssembler
{
    public static CreateSubscriptionPlanCommand ToCommandFromResource(CreateSubscriptionPlanResource resource)
    {
        return new CreateSubscriptionPlanCommand(
            
            resource.Name,
            resource.MaxRooms,
            resource.MaxStaffMembers,
            resource.MaxDevices,
            resource.Price,
            resource.Currency);
    }
}