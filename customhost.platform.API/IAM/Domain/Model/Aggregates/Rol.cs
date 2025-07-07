using customhost_backend.IAM.Domain.Model.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace customhost_backend.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *     The role aggregate
 * </summary>
 * <remarks>
 *     This class represents a role that can be assigned to users
 * </remarks>
 */
[Table("roles")]
public class Rol
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required]
    [Column("role_name")]
    public ERoles RoleName { get; private set; }

    /**
     * <summary>
     *     Default constructor for EF Core
     * </summary>
     */
    protected Rol()
    {
    }

    /**
     * <summary>
     *     Constructor for creating a new role
     * </summary>
     * <param name="roleName">The role name</param>
     */
    public Rol(ERoles roleName)
    {
        RoleName = roleName;
    }

    /**
     * <summary>
     *     Update the role name
     * </summary>
     * <param name="roleName">The new role name</param>
     * <returns>The updated role</returns>
     */
    public Rol UpdateRoleName(ERoles roleName)
    {
        RoleName = roleName;
        return this;
    }
}
