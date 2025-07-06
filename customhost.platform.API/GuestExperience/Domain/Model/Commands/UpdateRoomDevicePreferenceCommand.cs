using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to update a Room Device Preference
/// </summary>
public record UpdateDevicePreferenceCommand(
    [Required] int Id,
    [Required] int DeviceId,
    [Required] string Preferences
);
