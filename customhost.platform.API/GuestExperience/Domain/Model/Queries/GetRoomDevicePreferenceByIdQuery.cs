using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get a Room Device Preference by Id
/// </summary>
public record GetDevicePreferenceByIdQuery(
    [Required] int Id
);
