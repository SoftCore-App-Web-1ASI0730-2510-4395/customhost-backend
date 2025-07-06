using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateDevicePreferenceResource to CreateDevicePreferenceCommand
/// </summary>
public static class CreateDevicePreferenceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateDevicePreferenceResource to CreateDevicePreferenceCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateDevicePreferenceResource"/> resource to convert</param>
    /// <returns><see cref="CreateDevicePreferenceCommand"/> converted from <see cref="CreateDevicePreferenceResource"/> resource</returns>
    public static CreateDevicePreferenceCommand ToCommandFromResource(CreateDevicePreferenceResource resource)
    {
        return new CreateDevicePreferenceCommand(
            resource.DeviceId,
            resource.Preferences
        );
    }
}
