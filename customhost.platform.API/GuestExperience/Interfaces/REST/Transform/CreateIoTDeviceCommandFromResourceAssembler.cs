using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateDeviceModelResource to CreateDeviceModelCommand
/// </summary>
public static class CreateDeviceModelCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateDeviceModelResource to CreateDeviceModelCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateDeviceModelResource"/> resource to convert</param>
    /// <returns><see cref="CreateDeviceModelCommand"/> converted from <see cref="CreateDeviceModelResource"/> resource</returns>
    public static CreateDeviceModelCommand ToCommandFromResource(CreateDeviceModelResource resource)
    {
        return new CreateDeviceModelCommand(
            resource.Name,
            resource.DeviceType,
            resource.ConfigSchema
        );
    }
}
