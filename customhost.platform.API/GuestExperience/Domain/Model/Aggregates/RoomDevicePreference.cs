using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Model.Aggregates;

/// <summary>
/// Room Device Preference aggregate root that stores configuration preferences for a specific room device
/// </summary>
public class DevicePreference
{
    public int Id { get; private set; }
    public int DeviceId { get; private set; }
    public string Preferences { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdated { get; private set; }

    // Navigation properties
    public virtual Device Device { get; private set; }

    // For EF Core
    protected DevicePreference() { }

    public DevicePreference(int DeviceId, string preferences)
    {
        if (DeviceId <= 0)
            throw new ArgumentException("Room Device ID must be greater than zero", nameof(DeviceId));

        DeviceId = DeviceId;
        Preferences = preferences ?? "{}";
        CreatedAt = DateTime.UtcNow;
        LastUpdated = DateTime.UtcNow;
    }

    public DevicePreference(CreateDevicePreferenceCommand command) : this(command.DeviceId, command.Preferences)
    {
    }

    public void UpdatePreferences(string preferences)
    {
        Preferences = preferences ?? "{}";
        LastUpdated = DateTime.UtcNow;
    }
}
