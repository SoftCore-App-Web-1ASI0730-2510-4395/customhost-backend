using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByEmailAsync(string email);
    Task<IEnumerable<Profile>> FindByHotelIdAsync(int hotelId);
    Task<IEnumerable<Profile>> FindByRoleAsync(EProfileRole role);

    Task<Profile?> FindByUserIdAsync(int userId);
}