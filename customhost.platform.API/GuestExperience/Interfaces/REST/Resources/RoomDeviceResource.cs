namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Room Device resource for API responses
/// </summary>
public record DeviceResource(
    int Id,
    int RoomId,
    int DeviceModelId,
    string Status,
    DateTime CreatedAt,
    DeviceModelResource DeviceModel
);
