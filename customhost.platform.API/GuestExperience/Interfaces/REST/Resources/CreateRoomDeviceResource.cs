namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Create Room Device resource for API requests
/// </summary>
public record CreateDeviceResource(
    int RoomId,
    int DeviceModelId,
    string Status = "working"
);
