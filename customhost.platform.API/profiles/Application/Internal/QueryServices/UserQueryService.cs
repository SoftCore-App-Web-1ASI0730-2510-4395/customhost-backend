using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;
using customhost.platform.API.profiles.Domain.Queries;

namespace customhost_backend.profiles.Application.Internal.QueryServices;

/// <summary>
/// Profile Query Service Implementation
/// </summary>
public class ProfileQueryService(IProfileRepository ProfileRepository) 
    : IProfileQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Profile>> GetAllAsync()
    {
        return await ProfileRepository.ListAsync();
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await ProfileRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Profile?> GetByIdAsync(int id)
    {
        return await ProfileRepository.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<Profile?> GetByEmailAsync(string email)
    {
        return await ProfileRepository.FindByEmailAsync(email);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Profile>> GetByHotelIdAsync(int hotelId)
    {
        return await ProfileRepository.FindByHotelIdAsync(hotelId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Profile>> GetByRoleAsync(EProfileRole role)
    {
        return await ProfileRepository.FindByRoleAsync(role);
    }
}