namespace customhost_backend.billings.Interfaces.REST.Resources;

public record SubscriptionResource(int Id,
    int HotelId,
    int SubscriptionPlanId,
    string Status,
    DateTime StartDate,
    DateTime EndDate);