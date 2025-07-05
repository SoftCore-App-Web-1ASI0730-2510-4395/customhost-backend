using customhost_backend.IAM.Domain.Model.Queries;
using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost.platform.API.profiles.Domain.Queries;

namespace customhost_backend.profiles.Domain.Services;

/// <summary>
/// Profile Query Service Interface
/// </summary>
public interface IProfileQueryService
{
    /// <summary>
    /// Get all Profiles
    /// </summary>
    /// <returns>All Profiles</returns>
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    
    /// <summary>
    /// Get Profile by ID
    /// </summary>
    /// <param name="id">Profile ID</param>
    /// <returns>Profile or null if not found</returns>
    Task<Profile?> GetByIdAsync(int id);
    
    /// <summary>
    /// Get Profile by email
    /// </summary>
    /// <param name="email">Profile email</param>
    /// <returns>Profile or null if not found</returns>
    Task<Profile?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Get Profiles by hotel ID
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>Hotel Profiles</returns>
    Task<IEnumerable<Profile>> GetByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Get Profiles by role
    /// </summary>
    /// <param name="role">Profile role</param>
    /// <returns>Profiles with the specified role</returns>
    Task<IEnumerable<Profile>> GetByRoleAsync(EProfileRole role);
}