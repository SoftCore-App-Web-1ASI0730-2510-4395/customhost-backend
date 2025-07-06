using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Device entity to DeviceResource
/// </summary>
public static class DeviceResourceFromEntityAssembler
{
    /// <summary>
    /// Convert Device entity to DeviceResource
    /// </summary>
    /// <param name="entity"><see cref="Device"/> entity to convert</param>
    /// <returns><see cref="DeviceResource"/> converted from <see cref="Device"/> entity</returns>
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        var DeviceModelResource = entity.DeviceModel != null 
            ? DeviceModelResourceFromEntityAssembler.ToResourceFromEntity(entity.DeviceModel)
            : new DeviceModelResource(0, "", "", "", "", DateTime.MinValue);

        return new DeviceResource(
            entity.Id,
            entity.RoomId,
            entity.DeviceModelId,
            entity.Status,
            entity.CreatedAt,
            DeviceModelResource
        );
    }
}
