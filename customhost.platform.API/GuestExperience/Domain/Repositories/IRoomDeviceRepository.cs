using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for Room Device aggregate
/// </summary>
public interface IDeviceRepository : IBaseRepository<Device>
{
    Task<IEnumerable<Device>> FindByRoomIdAsync(int roomId);
    Task<IEnumerable<Device>> FindByDeviceModelIdAsync(int DeviceModelId);
    Task<bool> ExistsDeviceInRoomAsync(int roomId, int DeviceModelId);
}
