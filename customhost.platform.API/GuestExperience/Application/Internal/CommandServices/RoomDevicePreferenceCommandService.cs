using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// Room Device Preference command service implementation
/// </summary>
public class DevicePreferenceCommandService(
    IDevicePreferenceRepository DevicePreferenceRepository,
    IDeviceRepository DeviceRepository,
    IUnitOfWork unitOfWork
) : IDevicePreferenceCommandService
{
    public async Task<DevicePreference?> Handle(CreateDevicePreferenceCommand command)
    {
        // Verify Room Device exists
        var Device = await DeviceRepository.FindByIdAsync(command.DeviceId);
        if (Device is null)
            throw new Exception($"Room Device with id {command.DeviceId} not found");

        // Check if preference already exists for this room device
        var existingPreference = await DevicePreferenceRepository.FindByDeviceIdSingleAsync(command.DeviceId);
        if (existingPreference is not null)
            throw new Exception($"Preference for Room Device {command.DeviceId} already exists");

        var DevicePreference = new DevicePreference(command);
        await DevicePreferenceRepository.AddAsync(DevicePreference);
        await unitOfWork.CompleteAsync();
        return DevicePreference;
    }

    public async Task<DevicePreference?> Handle(UpdateDevicePreferenceCommand command)
    {
        var DevicePreference = await DevicePreferenceRepository.FindByIdAsync(command.Id);
        if (DevicePreference is null)
            throw new Exception($"Room Device Preference with id {command.Id} not found");

        // Verify Room Device exists
        var Device = await DeviceRepository.FindByIdAsync(command.DeviceId);
        if (Device is null)
            throw new Exception($"Room Device with id {command.DeviceId} not found");

        DevicePreference.UpdatePreferences(command.Preferences);
        await unitOfWork.CompleteAsync();
        return DevicePreference;
    }
}
