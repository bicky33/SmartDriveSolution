using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Users;

[Table("refresh_token", Schema = "users")]
[Index("RetoToken", Name = "uq_reto_token", IsUnique = true)]
public partial class RefreshToken
{
    [Key]
    [Column("reto_id")]
    public int RetoId { get; set; }

    [Column("reto_user_id")]
    public int? RetoUserId { get; set; }

    [Column("reto_token")]
    [StringLength(125)]
    [Unicode(false)]
    public string? RetoToken { get; set; }

    [Column("reto_expire_date", TypeName = "date")]
    public DateTime? RetoExpireDate { get; set; }

    [ForeignKey("RetoUserId")]
    [InverseProperty("RefreshTokens")]
    public virtual User? RetoUser { get; set; }
}
