using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile Profile) =>
         
        new ProfileResource(
            Profile.Id,
            Profile.HotelId,
            Profile.FirstName,
            Profile.LastName,
            Profile.Email,
            Profile.Phone,
            Profile.Role.ToString(),
            Profile.CreatedAt,
            Profile.UserId
        );
    
    public static IEnumerable<ProfileResource> ToResourcesFromEntities(IEnumerable<Profile> profiles) =>
        profiles.Select(ToResourceFromEntity);
}