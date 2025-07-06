using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device query service interface
/// </summary>
public interface IDeviceQueryService
{
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query);
    Task<Device?> Handle(GetDeviceByIdQuery query);
    Task<IEnumerable<Device>> Handle(GetDevicesByRoomIdQuery query);
}
