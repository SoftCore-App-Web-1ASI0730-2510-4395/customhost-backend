using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        if (!Enum.TryParse<EProfileRole>(resource.Role, true, out var role))
            role = EProfileRole.Guest;

        return new CreateProfileCommand(
            resource.HotelId ?? 0,
            resource.FirstName ?? "",
            resource.LastName ?? "",
            resource.Email ?? "",
            resource.Password ?? "",
            resource.Phone ?? "",
            role
        );
    }
}