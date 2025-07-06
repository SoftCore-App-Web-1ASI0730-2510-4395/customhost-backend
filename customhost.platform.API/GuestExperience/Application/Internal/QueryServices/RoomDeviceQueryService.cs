using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;

namespace customhost_backend.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// Room Device query service implementation
/// </summary>
public class DeviceQueryService(IDeviceRepository DeviceRepository) : IDeviceQueryService
{
    public async Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query)
    {
        return await DeviceRepository.ListAsync();
    }

    public async Task<Device?> Handle(GetDeviceByIdQuery query)
    {
        return await DeviceRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Device>> Handle(GetDevicesByRoomIdQuery query)
    {
        return await DeviceRepository.FindByRoomIdAsync(query.RoomId);
    }
}
