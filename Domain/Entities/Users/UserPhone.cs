using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Users;

[PrimaryKey("UsphEntityid", "UsphPhoneNumber")]
[Table("user_phone", Schema = "users")]
public partial class UserPhone
{
    [Key]
    [Column("usph_entityid")]
    public int UsphEntityid { get; set; }

    [Key]
    [Column("usph_phone_number")]
    [StringLength(15)]
    [Unicode(false)]
    public string UsphPhoneNumber { get; set; } = null!;

    [Column("usph_phone_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UsphPhoneType { get; set; }

    [Column("usph_mime")]
    [StringLength(512)]
    [Unicode(false)]
    public string? UsphMime { get; set; }

    [Column("usph_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UsphStatus { get; set; }

    [Column("usph_modified_date", TypeName = "datetime")]
    public DateTime? UsphModifiedDate { get; set; }

    [ForeignKey("UsphEntityid")]
    [InverseProperty("UserPhones")]
    public virtual User UsphEntity { get; set; } = null!;
}
