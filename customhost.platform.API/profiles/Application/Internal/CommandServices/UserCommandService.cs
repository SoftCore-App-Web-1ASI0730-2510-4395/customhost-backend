using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.profiles.Application.Internal.CommandServices;

/// <summary>
/// Profile Command Service Implementation
/// </summary>
public class ProfileCommandService(IProfileRepository ProfileRepository, IUnitOfWork unitOfWork) 
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        try
        {
            // Check if email already exists
            var existingProfile = await ProfileRepository.FindByEmailAsync(command.Email);
            if (existingProfile != null) return null;

            var Profile = new Profile(command);
            await ProfileRepository.AddAsync(Profile);
            await unitOfWork.CompleteAsync();
            return Profile;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(UpdateProfileCommand command)
    {
        try
        {
            var Profile = await ProfileRepository.FindByIdAsync(command.Id);
            if (Profile == null) return null;

            // Check if email is being changed and if it already exists
            if (!string.IsNullOrWhiteSpace(command.Email) && command.Email != Profile.Email)
            {
                var existingProfile = await ProfileRepository.FindByEmailAsync(command.Email);
                if (existingProfile != null) return null;
            }

            Profile.UpdateProfile(command);
            ProfileRepository.Update(Profile);
            await unitOfWork.CompleteAsync();
            return Profile;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteProfileCommand command)
    {
        try
        {
            var Profile = await ProfileRepository.FindByIdAsync(command.Id);
            if (Profile == null) return false;

            ProfileRepository.Remove(Profile);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}