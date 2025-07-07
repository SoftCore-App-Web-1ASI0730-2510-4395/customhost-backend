using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert UpdateDeviceModelResource to UpdateDeviceModelCommand
/// </summary>
public static class UpdateDeviceModelCommandFromResourceAssembler
{
    /// <summary>
    /// Convert UpdateDeviceModelResource to UpdateDeviceModelCommand
    /// </summary>
    /// <param name="id">The ID of the IoT device to update</param>
    /// <param name="resource"><see cref="UpdateDeviceModelResource"/> resource to convert</param>
    /// <returns><see cref="UpdateDeviceModelCommand"/> converted from <see cref="UpdateDeviceModelResource"/> resource</returns>
    public static UpdateDeviceModelCommand ToCommandFromResource(int id, UpdateDeviceModelResource resource)
    {
        return new UpdateDeviceModelCommand(
            id,
            resource.Name,
            resource.DeviceType,
            resource.ConfigSchema
        );
    }
}
