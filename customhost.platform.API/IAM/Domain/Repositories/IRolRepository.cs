using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Domain.Model.ValueObjects;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.IAM.Domain.Repositories;

/**
 * <summary>
 *     The role repository interface
 * </summary>
 * <remarks>
 *     This interface provides access to the role repository
 * </remarks>
 */
public interface IRolRepository : IBaseRepository<Rol>
{
    /**
     * <summary>
     *     Find a role by its name
     * </summary>
     * <param name="roleName">The role name</param>
     * <returns>The role if found, null otherwise</returns>
     */
    Task<Rol?> FindByRoleNameAsync(ERoles roleName);
    
    /**
     * <summary>
     *     Check if a role exists by its name
     * </summary>
     * <param name="roleName">The role name</param>
     * <returns>True if the role exists, false otherwise</returns>
     */
    bool ExistsByRoleName(ERoles roleName);
}
