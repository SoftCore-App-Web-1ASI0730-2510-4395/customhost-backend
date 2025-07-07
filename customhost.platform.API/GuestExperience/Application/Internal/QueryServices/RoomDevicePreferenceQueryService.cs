using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;

namespace customhost_backend.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// Room Device Preference query service implementation
/// </summary>
public class DevicePreferenceQueryService(IDevicePreferenceRepository DevicePreferenceRepository) : IDevicePreferenceQueryService
{
    public async Task<IEnumerable<DevicePreference>> Handle(GetAllDevicePreferencesQuery query)
    {
        return await DevicePreferenceRepository.ListAsync();
    }

    public async Task<DevicePreference?> Handle(GetDevicePreferenceByIdQuery query)
    {
        return await DevicePreferenceRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<DevicePreference>> Handle(GetDevicePreferencesByDeviceIdQuery query)
    {
        return await DevicePreferenceRepository.FindByDeviceIdAsync(query.DeviceId);
    }
}
