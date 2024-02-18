using Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Users;

[PrimaryKey("UsdrId", "UsdrEntityid")]
[Table("user_address", Schema = "users")]
public partial class UserAddress
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    [Column("usdr_id")]
    public int UsdrId { get; set; }

    [Key]
    [Column("usdr_entityid")]
    public int UsdrEntityid { get; set; }

    [Column("usdr_address1")]
    [StringLength(255)]
    [Unicode(false)]
    public string? UsdrAddress1 { get; set; }

    [Column("usdr_address2")]
    [StringLength(255)]
    [Unicode(false)]
    public string? UsdrAddress2 { get; set; }

    [Column("usdr_modified_date", TypeName = "datetime")]
    public DateTime? UsdrModifiedDate { get; set; }

    [Column("usdr_city_id")]
    public int? UsdrCityId { get; set; }

    [ForeignKey("UsdrCityId")]
    [InverseProperty("UserAddresses")]
    public virtual City? UsdrCity { get; set; }

    [ForeignKey("UsdrEntityid")]
    [InverseProperty("UserAddresses")]
    public virtual User UsdrEntity { get; set; } = null!;
}