using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for Room Device Preference aggregate
/// </summary>
public interface IDevicePreferenceRepository : IBaseRepository<DevicePreference>
{
    Task<IEnumerable<DevicePreference>> FindByDeviceIdAsync(int DeviceId);
    Task<DevicePreference?> FindByDeviceIdSingleAsync(int DeviceId);
}
