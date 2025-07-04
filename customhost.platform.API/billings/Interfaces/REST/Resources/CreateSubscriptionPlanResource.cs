namespace customhost_backend.billings.Interfaces.REST.Resources;

public record CreateSubscriptionPlanResource(
    
    string Name,
    int MaxRooms,
    int MaxStaffMembers,
    int MaxDevices,
    decimal Price,
    string Currency
);