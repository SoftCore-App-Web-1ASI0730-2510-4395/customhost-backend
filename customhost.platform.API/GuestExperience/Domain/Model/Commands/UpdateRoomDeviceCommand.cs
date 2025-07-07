using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to update an existing Room Device
/// </summary>
public record UpdateDeviceCommand(
    [Required] int Id,
    [Required] int RoomId,
    [Required] int DeviceModelId,
    [Required] string Status
);
