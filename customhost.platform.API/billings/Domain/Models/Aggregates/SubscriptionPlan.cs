using customhost_backend.billings.Domain.Models.Commands;

namespace customhost_backend.billings.Domain.Models.Aggregates;

public class SubscriptionPlan
{
    public int Id { get; }
    public string Name { get; set; }
    public int MaxRooms { get; set; }
    public int MaxStaffMembers { get; set; }
    public int MaxDevices { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    
    public SubscriptionPlan() {}

    public SubscriptionPlan(CreateSubscriptionPlanCommand command)
    {
        Name = command.Name;
        MaxRooms = command.MaxRooms;
        MaxStaffMembers = command.MaxStaffMembers;
        MaxDevices = command.MaxDevices;
        Price = command.Price;
        Currency = command.Currency;
    }
    
}