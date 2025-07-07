using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert DevicePreference entity to DevicePreferenceResource
/// </summary>
public static class DevicePreferenceResourceFromEntityAssembler
{
    /// <summary>
    /// Convert DevicePreference entity to DevicePreferenceResource
    /// </summary>
    /// <param name="entity"><see cref="DevicePreference"/> entity to convert</param>
    /// <returns><see cref="DevicePreferenceResource"/> converted from <see cref="DevicePreference"/> entity</returns>
    public static DevicePreferenceResource ToResourceFromEntity(DevicePreference entity)
    {
        return new DevicePreferenceResource(
            entity.Id,
            entity.DeviceId,
            entity.Preferences,
            entity.CreatedAt
        );
    }
}
