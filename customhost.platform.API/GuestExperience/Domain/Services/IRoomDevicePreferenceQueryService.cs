using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device Preference query service interface
/// </summary>
public interface IDevicePreferenceQueryService
{
    Task<IEnumerable<DevicePreference>> Handle(GetAllDevicePreferencesQuery query);
    Task<DevicePreference?> Handle(GetDevicePreferenceByIdQuery query);
    Task<IEnumerable<DevicePreference>> Handle(GetDevicePreferencesByDeviceIdQuery query);
}
