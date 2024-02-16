using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Users;

[PrimaryKey("UsroEntityid", "UsroRoleName")]
[Table("user_roles", Schema = "users")]
public partial class UserRole
{
    [Key]
    [Column("usro_entityid")]
    public int UsroEntityid { get; set; }

    [Key]
    [Column("usro_role_name")]
    [StringLength(2)]
    [Unicode(false)]
    public string UsroRoleName { get; set; } = null!;

    [Column("usro_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UsroStatus { get; set; }

    [Column("usro_modified_date", TypeName = "datetime")]
    public DateTime? UsroModifiedDate { get; set; }

    [ForeignKey("UsroEntityid")]
    [InverseProperty("UserRoles")]
    public virtual User UsroEntity { get; set; } = null!;

    [ForeignKey("UsroRoleName")]
    [InverseProperty("UserRoles")]
    public virtual Role UsroRoleNameNavigation { get; set; } = null!;
}