using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Domain.Model.ValueObjects;
using customhost_backend.IAM.Domain.Repositories;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     Service to initialize default roles in the system
 * </summary>
 */
public class RoleInitializationService(IRolRepository rolRepository, IUnitOfWork unitOfWork)
{
    /**
     * <summary>
     *     Initialize default roles if they don't exist
     * </summary>
     */
    public async Task InitializeDefaultRolesAsync()
    {
        var rolesToCreate = new[]
        {
            ERoles.GUEST,
            ERoles.STAFF,
            ERoles.ADMIN
        };

        foreach (var roleEnum in rolesToCreate)
        {
            if (!rolRepository.ExistsByRoleName(roleEnum))
            {
                var role = new Rol(roleEnum);
                await rolRepository.AddAsync(role);
            }
        }

        await unitOfWork.CompleteAsync();
    }
}
