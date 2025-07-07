using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert DeviceModel entity to DeviceModelResource
/// </summary>
public static class DeviceModelResourceFromEntityAssembler
{
    /// <summary>
    /// Convert DeviceModel entity to DeviceModelResource
    /// </summary>
    /// <param name="entity"><see cref="DeviceModel"/> entity to convert</param>
    /// <returns><see cref="DeviceModelResource"/> converted from <see cref="DeviceModel"/> entity</returns>
    public static DeviceModelResource ToResourceFromEntity(DeviceModel entity)
    {
        return new DeviceModelResource(
            entity.Id,
            entity.Name,
            entity.DeviceType,
            entity.ConfigSchema,
            entity.Status,
            entity.CreatedAt
        );
    }
}
