using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device Preference command service interface
/// </summary>
public interface IDevicePreferenceCommandService
{
    Task<DevicePreference?> Handle(CreateDevicePreferenceCommand command);
    Task<DevicePreference?> Handle(UpdateDevicePreferenceCommand command);
}
