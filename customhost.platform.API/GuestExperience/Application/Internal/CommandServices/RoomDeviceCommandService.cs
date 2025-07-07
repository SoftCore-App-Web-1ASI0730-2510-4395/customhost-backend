using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// Room Device command service implementation
/// </summary>
public class DeviceCommandService(
    IDeviceRepository DeviceRepository,
    IDeviceModelRepository DeviceModelRepository,
    IUnitOfWork unitOfWork
) : IDeviceCommandService
{

    public async Task<Device> Handle(ChangeStatusDeviceCommand command)
    {
        var Device = await DeviceRepository.FindByIdAsync(command.Id);
        if (Device == null) return null;

        Device.ChangeStatus(command.Status);
        DeviceRepository.Update(Device);
        await unitOfWork.CompleteAsync();
        return Device;
    }
    
    public async Task<Device?> Handle(CreateDeviceCommand command)
    {
        // Verify IoT Device exists
        var DeviceModel = await DeviceModelRepository.FindByIdAsync(command.DeviceModelId);
        if (DeviceModel is null)
            throw new Exception($"IoT Device with id {command.DeviceModelId} not found");

        // Check if device is already assigned to this room
        if (await DeviceRepository.ExistsDeviceInRoomAsync(command.RoomId, command.DeviceModelId))
            throw new Exception($"IoT Device {command.DeviceModelId} is already assigned to room {command.RoomId}");

        var Device = new Device(command);
        await DeviceRepository.AddAsync(Device);
        await unitOfWork.CompleteAsync();
        return Device;
    }

    public async Task<Device?> Handle(UpdateDeviceCommand command)
    {
        var Device = await DeviceRepository.FindByIdAsync(command.Id);
        if (Device is null)
            throw new Exception($"Room Device with id {command.Id} not found");

        // Verify IoT Device exists
        var DeviceModel = await DeviceModelRepository.FindByIdAsync(command.DeviceModelId);
        if (DeviceModel is null)
            throw new Exception($"IoT Device with id {command.DeviceModelId} not found");

        // Check if we're changing the assignment and if the new assignment conflicts
        if ((Device.RoomId != command.RoomId || Device.DeviceModelId != command.DeviceModelId) &&
            await DeviceRepository.ExistsDeviceInRoomAsync(command.RoomId, command.DeviceModelId))
            throw new Exception($"IoT Device {command.DeviceModelId} is already assigned to room {command.RoomId}");

        Device.UpdateStatus(command.Status);
        await unitOfWork.CompleteAsync();
        return Device;
    }

    public async Task<bool> Handle(DeleteDeviceCommand command)
    {
        var Device = await DeviceRepository.FindByIdAsync(command.Id);
        if (Device is null)
            throw new Exception($"Room Device with id {command.Id} not found");

        DeviceRepository.Remove(Device);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
