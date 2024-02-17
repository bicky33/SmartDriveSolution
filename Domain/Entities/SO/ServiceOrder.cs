using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.HR;
using Domain.Entities.Partners;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.SO;

[Table("service_orders", Schema = "so")]
public partial class ServiceOrder
{
    [Key]
    [Column("sero_id")]
    [StringLength(25)]
    [Unicode(false)]
    public string SeroId { get; set; } = null!;

    [Column("sero_ordt_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SeroOrdtType { get; set; }

    [Column("sero_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SeroStatus { get; set; }

    [Column("sero_reason")]
    [StringLength(256)]
    [Unicode(false)]
    public string? SeroReason { get; set; }

    [Column("serv_claim_no")]
    [StringLength(12)]
    [Unicode(false)]
    public string? ServClaimNo { get; set; }

    [Column("serv_claim_startdate", TypeName = "datetime")]
    public DateTime? ServClaimStartdate { get; set; }

    [Column("serv_claim_enddate", TypeName = "datetime")]
    public DateTime? ServClaimEnddate { get; set; }

    [Column("sero_serv_id")]
    public int? SeroServId { get; set; }

    [Column("sero_sero_id")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SeroSeroId { get; set; }

    [Column("sero_agent_entityid")]
    public int? SeroAgentEntityid { get; set; }

    [Column("sero_part_id")]
    public int? SeroPartId { get; set; }

    [InverseProperty("BpinSero")]
    public virtual ICollection<BatchPartnerInvoice> BatchPartnerInvoices { get; set; } = new List<BatchPartnerInvoice>();

    [InverseProperty("CaevSero")]
    public virtual ICollection<ClaimAssetEvidence> ClaimAssetEvidences { get; set; } = new List<ClaimAssetEvidence>();

    [InverseProperty("CaspSero")]
    public virtual ICollection<ClaimAssetSparepart> ClaimAssetSpareparts { get; set; } = new List<ClaimAssetSparepart>();

    [InverseProperty("SeroSero")]
    public virtual ICollection<ServiceOrder> InverseSeroSero { get; set; } = new List<ServiceOrder>();

    public virtual EmployeeAreWorkgroup? SeroAgentEntity { get; set; }

    [ForeignKey("SeroPartId")]
    [InverseProperty("ServiceOrders")]
    public virtual Partner? SeroPart { get; set; }

    [ForeignKey("SeroSeroId")]
    [InverseProperty("InverseSeroSero")]
    public virtual ServiceOrder? SeroSero { get; set; }

    [ForeignKey("SeroServId")]
    [InverseProperty("ServiceOrders")]
    public virtual Servic? SeroServ { get; set; }

    [InverseProperty("SeotSero")]
    public virtual ICollection<ServiceOrderTask> ServiceOrderTasks { get; set; } = new List<ServiceOrderTask>();
}
