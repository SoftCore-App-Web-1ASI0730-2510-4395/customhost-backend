using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device command service interface
/// </summary>
public interface IDeviceCommandService
{
    Task<Device?> Handle(CreateDeviceCommand command);
    Task<Device?> Handle(UpdateDeviceCommand command);
    
    Task<Device?> Handle(ChangeStatusDeviceCommand command);
    Task<bool> Handle(DeleteDeviceCommand command);
}
