namespace customhost_backend.billings.Domain.Models.Commands;

public record CreateSubscriptionCommand(int HotelId, int SubscriptionPlanId, string Status, DateTime StartDate, DateTime EndDate);
