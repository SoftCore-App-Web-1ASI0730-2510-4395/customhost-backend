using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to create a new Room Device
/// </summary>
public record CreateDeviceCommand(
    [Required] int RoomId,
    [Required] int DeviceModelId,
    [Required] string Status = "working"
);
