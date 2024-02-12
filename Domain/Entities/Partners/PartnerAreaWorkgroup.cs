using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Partners;

[PrimaryKey("PawoPatrEntityid", "PawoArwgCode", "PawoUserEntityid")]
[Table("partner_area_workgroup", Schema = "partners")]
public partial class PartnerAreaWorkgroup
{
    [Key]
    [Column("pawo_patr_entityid")]
    public int PawoPatrEntityid { get; set; }

    [Key]
    [Column("pawo_arwg_code")]
    [StringLength(15)]
    [Unicode(false)]
    public string PawoArwgCode { get; set; } = null!;

    [Key]
    [Column("pawo_user_entityid")]
    public int PawoUserEntityid { get; set; }

    [Column("pawo_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PawoStatus { get; set; }

    [Column("pawo_modified_date", TypeName = "datetime")]
    public DateTime? PawoModifiedDate { get; set; }

    [ForeignKey("PawoPatrEntityid, PawoUserEntityid")]
    [InverseProperty("PartnerAreaWorkgroups")]
    public virtual PartnerContact Pawo { get; set; } = null!;

    [ForeignKey("PawoArwgCode")]
    [InverseProperty("PartnerAreaWorkgroups")]
    public virtual AreaWorkgroup PawoArwgCodeNavigation { get; set; } = null!;
}
