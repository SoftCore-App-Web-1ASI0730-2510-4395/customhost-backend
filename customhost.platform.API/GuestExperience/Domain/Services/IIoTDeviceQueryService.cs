using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// IoT Device query service interface
/// </summary>
public interface IDeviceModelQueryService
{
    Task<IEnumerable<DeviceModel>> Handle(GetAllDeviceModelsQuery query);
    Task<DeviceModel?> Handle(GetDeviceModelByIdQuery query);
}
