using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// IoT Device repository implementation using Entity Framework Core
/// </summary>
public class DeviceModelRepository(AppDbContext context) : BaseRepository<DeviceModel>(context), IDeviceModelRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Set<DeviceModel>().AnyAsync(d => d.Name == name);
    }

    public async Task<IEnumerable<DeviceModel>> FindByDeviceTypeAsync(string deviceType)
    {
        return await Context.Set<DeviceModel>()
            .Where(d => d.DeviceType == deviceType)
            .ToListAsync();
    }
}
