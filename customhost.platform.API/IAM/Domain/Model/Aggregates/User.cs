    using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace customhost_backend.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
[Table("users")]
public partial class User(string username, string passwordHash, int rolId)
{
    public User() : this(string.Empty, string.Empty, 1) // Default to role ID 1 (GUEST)
    {
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }
    
    [Required]
    public string Username { get; private set; } = username;

    [JsonIgnore] 
    [Required]
    public string PasswordHash { get; private set; } = passwordHash;

    [Column("rol_id")]
    [ForeignKey("Rol")]
    [Required]
    public int RolId { get; private set; } = rolId;

    // Navigation property
    [Required]
    public virtual Rol Rol { get; set; } = null!;

    /**
     * <summary>
     *     Update the username
     * </summary>
     * <param name="username">The new username</param>
     * <returns>The updated user</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    /**
     * <summary>
     *     Update the role
     * </summary>
     * <param name="rolId">The new role ID</param>
     * <returns>The updated user</returns>
     */
    public User UpdateRole(int rolId)
    {
        RolId = rolId;
        return this;
    }
}