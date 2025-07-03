using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile Profile)
    {
        return new ProfileResource(
            Profile.Id,
            Profile.HotelId,
            Profile.FirstName,
            Profile.LastName,
            Profile.Email,
            Profile.Phone,
            Profile.Role.ToString(),
            Profile.CreatedAt
        );
    }

    public static List<ProfileResource> ToResourcesFromEntities(IEnumerable<Profile> Profiles)
    {
        return Profiles.Select(Profile => ToResourceFromEntity(Profile)).ToList();
    }
}