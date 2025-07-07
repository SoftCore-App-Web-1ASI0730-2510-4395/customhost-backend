using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to delete a Room Device
/// </summary>
public record DeleteDeviceCommand(
    [Required] int Id
);
