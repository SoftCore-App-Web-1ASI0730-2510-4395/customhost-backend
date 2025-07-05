using customhost_backend.crm.Domain.Repositories;
using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.profiles.Application.Internal.CommandServices;

/// <summary>
/// Profile Command Service Implementation
/// </summary>
public class ProfileCommandService(IProfileRepository ProfileRepository, IUnitOfWork unitOfWork, IHotelRepository hotelRepository) 
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        
            // Check ig hotelId exists
            if (command.HotelId.HasValue)
            {
                var hotel = await hotelRepository.FindByIdAsync(command.HotelId.Value);
                if (hotel == null)
                    throw new Exception($"Hotel with ID {command.HotelId.Value} not found.");
            }

            // Check if email already exists
            var existingProfile = await ProfileRepository.FindByEmailAsync(command.Email);
            if (existingProfile != null) return null;

            var Profile = new Profile(command);
            await ProfileRepository.AddAsync(Profile);
            await unitOfWork.CompleteAsync();
            return Profile;
        
     
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(UpdateProfileCommand command)
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

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteProfileCommand command)
    {
        
            var Profile = await ProfileRepository.FindByIdAsync(command.Id);
            if (Profile == null) return false;

            ProfileRepository.Remove(Profile);
            await unitOfWork.CompleteAsync();
            return true;
        
    }
}