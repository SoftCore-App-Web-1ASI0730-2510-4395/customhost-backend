using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for IoT Device aggregate
/// </summary>
public interface IDeviceModelRepository : IBaseRepository<DeviceModel>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<DeviceModel>> FindByDeviceTypeAsync(string deviceType);
}
