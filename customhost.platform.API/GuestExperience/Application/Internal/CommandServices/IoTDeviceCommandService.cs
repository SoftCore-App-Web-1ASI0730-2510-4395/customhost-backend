using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// IoT Device command service implementation
/// </summary>
public class DeviceModelCommandService(
    IDeviceModelRepository DeviceModelRepository,
    IUnitOfWork unitOfWork
) : IDeviceModelCommandService
{
    public async Task<DeviceModel?> Handle(CreateDeviceModelCommand command)
    {
        if (await DeviceModelRepository.ExistsByNameAsync(command.Name))
            throw new Exception($"IoT Device with name {command.Name} already exists");

        var DeviceModel = new DeviceModel(command);
        await DeviceModelRepository.AddAsync(DeviceModel);
        await unitOfWork.CompleteAsync();
        return DeviceModel;
    }

    public async Task<DeviceModel?> Handle(UpdateDeviceModelCommand command)
    {
        var DeviceModel = await DeviceModelRepository.FindByIdAsync(command.Id);
        if (DeviceModel is null) 
            throw new Exception($"IoT Device with id {command.Id} not found");

        // Check if name is being changed and if new name already exists
        if (DeviceModel.Name != command.Name && await DeviceModelRepository.ExistsByNameAsync(command.Name))
            throw new Exception($"IoT Device with name {command.Name} already exists");

        DeviceModel.UpdateDevice(command.Name, command.DeviceType, command.ConfigSchema);
        await unitOfWork.CompleteAsync();
        return DeviceModel;
    }

    public async Task<bool> Handle(DeleteDeviceModelCommand command)
    {
        var DeviceModel = await DeviceModelRepository.FindByIdAsync(command.Id);
        if (DeviceModel is null) 
            throw new Exception($"IoT Device with id {command.Id} not found");

        DeviceModelRepository.Remove(DeviceModel);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
