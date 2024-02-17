using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.HR;
using Domain.Entities.SO;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.CR;

[Table("customer_request", Schema = "customer")]
public partial class CustomerRequest
{
    [Key]
    [Column("creq_entityid")]
    public int CreqEntityid { get; set; }

    [Column("creq_create_date", TypeName = "datetime")]
    public DateTime? CreqCreateDate { get; set; }

    [Column("creq_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CreqStatus { get; set; }

    [Column("creq_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CreqType { get; set; }

    [Column("creq_modified_date", TypeName = "datetime")]
    public DateTime? CreqModifiedDate { get; set; }

    [Column("creq_cust_entityid")]
    public int? CreqCustEntityid { get; set; }

    [Column("creq_agen_entityid")]
    public int? CreqAgenEntityid { get; set; }

    public virtual EmployeeAreWorkgroup? CreqAgenEntity { get; set; }

    [ForeignKey("CreqCustEntityid")]
    [InverseProperty("CustomerRequests")]
    public virtual User? CreqCustEntity { get; set; }

    [ForeignKey("CreqEntityid")]
    [InverseProperty("CustomerRequest")]
    public virtual BusinessEntity CreqEntity { get; set; } = null!;

    [InverseProperty("CuclCreqEntity")]
    public virtual CustomerClaim? CustomerClaim { get; set; }

    [InverseProperty("CiasCreqEntity")]
    public virtual CustomerInscAsset? CustomerInscAsset { get; set; }

    [InverseProperty("ServCreqEntity")]
    public virtual ICollection<Servic> Services { get; set; } = new List<Servic>();
}
