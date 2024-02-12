using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Master;
using Domain.Entities.SO;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Partners;

[Table("partners", Schema = "partners")]
public partial class Partner
{
    [Key]
    [Column("part_entityid")]
    public int PartEntityid { get; set; }

    [Column("part_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PartName { get; set; }

    [Column("part_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PartAddress { get; set; }

    [Column("part_join_date", TypeName = "datetime")]
    public DateTime? PartJoinDate { get; set; }

    [Column("part_accountNo")]
    [StringLength(35)]
    [Unicode(false)]
    public string? PartAccountNo { get; set; }

    [Column("part_npwp")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PartNpwp { get; set; }

    [Column("part_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PartStatus { get; set; }

    [Column("part_modified_date", TypeName = "datetime")]
    public DateTime? PartModifiedDate { get; set; }

    [Column("part_city_id")]
    public int PartCityId { get; set; }

    [InverseProperty("BpinPatrnEntity")]
    public virtual ICollection<BatchPartnerInvoice> BatchPartnerInvoices { get; set; } = new List<BatchPartnerInvoice>();

    [InverseProperty("CaevPartEntity")]
    public virtual ICollection<ClaimAssetEvidence> ClaimAssetEvidences { get; set; } = new List<ClaimAssetEvidence>();

    [InverseProperty("CaspPartEntity")]
    public virtual ICollection<ClaimAssetSparepart> ClaimAssetSpareparts { get; set; } = new List<ClaimAssetSparepart>();

    [ForeignKey("PartCityId")]
    [InverseProperty("Partners")]
    public virtual City PartCity { get; set; } = null!;

    [ForeignKey("PartEntityid")]
    [InverseProperty("Partner")]
    public virtual BusinessEntity PartEntity { get; set; } = null!;

    [InverseProperty("PacoPatrnEntity")]
    public virtual ICollection<PartnerContact> PartnerContacts { get; set; } = new List<PartnerContact>();

    [InverseProperty("SeroPart")]
    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}
