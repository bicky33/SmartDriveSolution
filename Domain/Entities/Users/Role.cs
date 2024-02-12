using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Users;

[Table("roles", Schema = "users")]
public partial class Role
{
    [Key]
    [Column("role_name")]
    [StringLength(2)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [Column("role_description")]
    [StringLength(35)]
    [Unicode(false)]
    public string RoleDescription { get; set; } = null!;

    [InverseProperty("UsroRoleNameNavigation")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
