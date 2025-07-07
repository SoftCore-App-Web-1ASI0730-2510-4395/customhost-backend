using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(int id, UpdateProfileResource resource)
    {
        EProfileRole? role = null;
        if (!string.IsNullOrEmpty(resource.Role) && Enum.TryParse<EProfileRole>(resource.Role, true, out var parsedRole))
            role = parsedRole;

        return new UpdateProfileCommand(
            id,
            resource.HotelId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            role,
            resource.UserId
        );
    }
}