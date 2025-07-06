using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Room Device repository implementation using Entity Framework Core
/// </summary>
public class DeviceRepository(AppDbContext context) : BaseRepository<Device>(context), IDeviceRepository
{
    public async Task<IEnumerable<Device>> FindByRoomIdAsync(int roomId)
    {
        return await Context.Set<Device>()
            .Include(rd => rd.DeviceModel)
            .Where(rd => rd.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Device>> FindByDeviceModelIdAsync(int DeviceModelId)
    {
        return await Context.Set<Device>()
            .Include(rd => rd.DeviceModel)
            .Where(rd => rd.DeviceModelId == DeviceModelId)
            .ToListAsync();
    }

    public async Task<bool> ExistsDeviceInRoomAsync(int roomId, int DeviceModelId)
    {
        return await Context.Set<Device>()
            .AnyAsync(rd => rd.RoomId == roomId && rd.DeviceModelId == DeviceModelId);
    }    public new async Task<Device?> FindByIdAsync(int id)
    {
        return await Context.Set<Device>()
            .Include(rd => rd.DeviceModel)
            .FirstOrDefaultAsync(rd => rd.Id == id);
    }

    public new async Task<IEnumerable<Device>> ListAsync()
    {
        return await Context.Set<Device>()
            .Include(rd => rd.DeviceModel)
            .ToListAsync();
    }
}
