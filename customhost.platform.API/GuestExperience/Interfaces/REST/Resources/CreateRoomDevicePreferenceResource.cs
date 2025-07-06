namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Create Room Device Preference resource for API requests
/// </summary>
public record CreateDevicePreferenceResource(
    int DeviceId,
    string Preferences
);
