using customhost_backend.profiles.Domain.Models.ValueObjects;

namespace customhost_backend.profiles.Domain.Models.Commands;

public record CreateProfileCommand(
    int? HotelId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    EProfileRole Role
);