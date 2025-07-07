namespace customhost_backend.billings.Domain.Models.Commands;

public record CreateSubscriptionPlanCommand(string Name, int MaxRooms, int MaxStaffMembers, int MaxDevices, decimal Price, string Currency);