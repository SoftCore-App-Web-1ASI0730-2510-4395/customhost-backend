using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// IoT Device command service interface
/// </summary>
public interface IDeviceModelCommandService
{
    Task<DeviceModel?> Handle(CreateDeviceModelCommand command);
    Task<DeviceModel?> Handle(UpdateDeviceModelCommand command);
    Task<bool> Handle(DeleteDeviceModelCommand command);
}
