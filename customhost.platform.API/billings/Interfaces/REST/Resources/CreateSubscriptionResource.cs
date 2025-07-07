namespace customhost_backend.billings.Interfaces.REST.Resources;

public record CreateSubscriptionResource(
   
    int HotelId,
    int SubscriptionPlanId,
    string Status,
    DateTime StartDate,
    DateTime EndDate
);