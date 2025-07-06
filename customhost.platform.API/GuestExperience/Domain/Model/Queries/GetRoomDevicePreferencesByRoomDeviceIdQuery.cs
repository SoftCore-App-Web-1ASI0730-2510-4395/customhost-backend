using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get Room Device Preferences by Room Device Id
/// </summary>
public record GetDevicePreferencesByDeviceIdQuery(
    [Required] int DeviceId
);
