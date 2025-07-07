namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Room Device Preference resource for API responses
/// </summary>
public record DevicePreferenceResource(
    int Id,
    int DeviceId,
    string Preferences,
    DateTime CreatedAt
);
