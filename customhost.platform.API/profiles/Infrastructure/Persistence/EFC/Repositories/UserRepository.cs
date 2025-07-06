using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) 
    : BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<Profile?> FindByEmailAsync(string email)
    {
        return await Context.Set<Profile>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Profile?> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Profile>()
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }
    
    public async Task<IEnumerable<Profile>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<Profile>()
            .Where(u => u.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Profile>> FindByRoleAsync(EProfileRole role)
    {
        return await Context.Set<Profile>()
            .Where(u => u.Role == role)
            .ToListAsync();
    }
    
}