using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Domain.Model.ValueObjects;
using customhost_backend.IAM.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.IAM.Infrastructure.Persistence.EFC.Repositories;

/**
 * <summary>
 *     The role repository implementation
 * </summary>
 * <remarks>
 *     This class implements the role repository interface
 * </remarks>
 */
public class RolRepository(AppDbContext context) : BaseRepository<Rol>(context), IRolRepository
{
    /**
     * <summary>
     *     Find a role by its name
     * </summary>
     * <param name="roleName">The role name</param>
     * <returns>The role if found, null otherwise</returns>
     */
    public async Task<Rol?> FindByRoleNameAsync(ERoles roleName)
    {
        return await Context.Set<Rol>().FirstOrDefaultAsync(r => r.RoleName == roleName);
    }
    
    /**
     * <summary>
     *     Check if a role exists by its name
     * </summary>
     * <param name="roleName">The role name</param>
     * <returns>True if the role exists, false otherwise</returns>
     */
    public bool ExistsByRoleName(ERoles roleName)
    {
        return Context.Set<Rol>().Any(r => r.RoleName == roleName);
    }
}
