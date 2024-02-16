using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Partners;

[PrimaryKey("PacoPatrnEntityid", "PacoUserEntityid")]
[Table("partner_contacts", Schema = "partners")]
public partial class PartnerContact
{
    [Key]
    [Column("paco_patrn_entityid")]
    public int PacoPatrnEntityid { get; set; }

    [Key]
    [Column("paco_user_entityid")]
    public int PacoUserEntityid { get; set; }

    [Column("paco_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PacoStatus { get; set; }

    [ForeignKey("PacoPatrnEntityid")]
    [InverseProperty("PartnerContacts")]
    public virtual Partner PacoPatrnEntity { get; set; } = null!;

    [ForeignKey("PacoUserEntityid")]
    [InverseProperty("PartnerContacts")]
    public virtual User PacoUserEntity { get; set; } = null!;

    [InverseProperty("Pawo")]
    public virtual ICollection<PartnerAreaWorkgroup> PartnerAreaWorkgroups { get; set; } = new List<PartnerAreaWorkgroup>();
}