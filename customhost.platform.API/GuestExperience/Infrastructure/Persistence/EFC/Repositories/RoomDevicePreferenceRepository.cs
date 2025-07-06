using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Room Device Preference repository implementation using Entity Framework Core
/// </summary>
public class DevicePreferenceRepository(AppDbContext context) : BaseRepository<DevicePreference>(context), IDevicePreferenceRepository
{
    public async Task<IEnumerable<DevicePreference>> FindByDeviceIdAsync(int DeviceId)
    {
        return await Context.Set<DevicePreference>()
            .Where(rdp => rdp.DeviceId == DeviceId)
            .ToListAsync();
    }

    public async Task<DevicePreference?> FindByDeviceIdSingleAsync(int DeviceId)
    {
        return await Context.Set<DevicePreference>()
            .FirstOrDefaultAsync(rdp => rdp.DeviceId == DeviceId);
    }
}
