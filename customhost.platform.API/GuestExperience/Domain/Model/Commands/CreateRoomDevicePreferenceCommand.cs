using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to create a Room Device Preference
/// </summary>
public record CreateDevicePreferenceCommand(
    [Required] int DeviceId,
    [Required] string Preferences
);
