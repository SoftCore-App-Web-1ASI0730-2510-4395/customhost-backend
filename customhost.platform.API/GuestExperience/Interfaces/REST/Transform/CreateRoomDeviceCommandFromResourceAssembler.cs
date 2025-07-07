using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateDeviceResource to CreateDeviceCommand
/// </summary>
public static class CreateDeviceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateDeviceResource to CreateDeviceCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateDeviceResource"/> resource to convert</param>
    /// <returns><see cref="CreateDeviceCommand"/> converted from <see cref="CreateDeviceResource"/> resource</returns>
    public static CreateDeviceCommand ToCommandFromResource(CreateDeviceResource resource)
    {
        return new CreateDeviceCommand(
            resource.RoomId,
            resource.DeviceModelId,
            resource.Status
        );
    }
}
