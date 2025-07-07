using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;

namespace customhost_backend.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// IoT Device query service implementation
/// </summary>
public class DeviceModelQueryService(IDeviceModelRepository DeviceModelRepository) : IDeviceModelQueryService
{
    public async Task<IEnumerable<DeviceModel>> Handle(GetAllDeviceModelsQuery query)
    {
        return await DeviceModelRepository.ListAsync();
    }

    public async Task<DeviceModel?> Handle(GetDeviceModelByIdQuery query)
    {
        return await DeviceModelRepository.FindByIdAsync(query.Id);
    }
}
