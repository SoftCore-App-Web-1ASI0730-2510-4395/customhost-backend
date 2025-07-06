namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Update Room Device resource for API requests
/// </summary>
public record UpdateDeviceResource(
    int RoomId,
    int DeviceModelId,
    string Status
);
