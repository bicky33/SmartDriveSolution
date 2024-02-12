using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Payment;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Partners;

[Table("batch_partner_invoice", Schema = "partners")]
public partial class BatchPartnerInvoice
{
    [Key]
    [Column("bpin_invoiceNo")]
    [StringLength(30)]
    [Unicode(false)]
    public string BpinInvoiceNo { get; set; } = null!;

    [Column("bpin_created_on", TypeName = "datetime")]
    public DateTime? BpinCreatedOn { get; set; }

    [Column("bpin_subtotal", TypeName = "money")]
    public decimal? BpinSubtotal { get; set; }

    [Column("bpin_tax", TypeName = "money")]
    public decimal? BpinTax { get; set; }

    [Column("bpin_accountNo")]
    [StringLength(30)]
    [Unicode(false)]
    public string? BpinAccountNo { get; set; }

    [Column("bpin_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? BpinStatus { get; set; }

    [Column("bpin_paid_date", TypeName = "datetime")]
    public DateTime? BpinPaidDate { get; set; }

    [Column("bpin_patrn_entityid")]
    public int? BpinPatrnEntityid { get; set; }

    [Column("bpin_patr_trxno")]
    [StringLength(55)]
    [Unicode(false)]
    public string? BpinPatrTrxno { get; set; }

    [Column("bpin_sero_id")]
    [StringLength(25)]
    [Unicode(false)]
    public string BpinSeroId { get; set; } = null!;

    [ForeignKey("BpinPatrTrxno")]
    [InverseProperty("BatchPartnerInvoices")]
    public virtual PaymentTransaction? BpinPatrTrxnoNavigation { get; set; }

    [ForeignKey("BpinPatrnEntityid")]
    [InverseProperty("BatchPartnerInvoices")]
    public virtual Partner? BpinPatrnEntity { get; set; }

    [ForeignKey("BpinSeroId")]
    [InverseProperty("BatchPartnerInvoices")]
    public virtual ServiceOrder BpinSero { get; set; } = null!;
}
