using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.Commands;

namespace customhost_backend.profiles.Domain.Services;

/// <summary>
/// Profile Command Service Interface
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    /// Handle create Profile command
    /// </summary>
    /// <param name="command">Create Profile command</param>
    /// <returns>Created Profile or null if failed</returns>
    Task<Profile?> Handle(CreateProfileCommand command);
    
    /// <summary>
    /// Handle update Profile command
    /// </summary>
    /// <param name="command">Update Profile command</param>
    /// <returns>Updated Profile or null if failed</returns>
    Task<Profile?> Handle(UpdateProfileCommand command);
    
    /// <summary>
    /// Handle delete Profile command
    /// </summary>
    /// <param name="command">Delete Profile command</param>
    /// <returns>True if deleted, false otherwise</returns>
    Task<bool> Handle(DeleteProfileCommand command);
}