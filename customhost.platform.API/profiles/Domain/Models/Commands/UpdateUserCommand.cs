using customhost_backend.profiles.Domain.Models.ValueObjects;

namespace customhost_backend.profiles.Domain.Models.Commands;

public record UpdateProfileCommand(
    int Id,
    int? HotelId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    EProfileRole? Role
);