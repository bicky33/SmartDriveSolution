using Domain.Entities.Partners;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Payment;

[Table("payment_transactions", Schema = "payment")]
public partial class PaymentTransaction
{
    [Key]
    [Column("patr_trxno")]
    [StringLength(55)]
    [Unicode(false)]
    public string PatrTrxno { get; set; } = null!;

    [Column("patr_created_on", TypeName = "datetime")]
    public DateTime? PatrCreatedOn { get; set; }

    [Column("patr_debet", TypeName = "money")]
    public decimal? PatrDebet { get; set; }

    [Column("patr_credit", TypeName = "money")]
    public decimal? PatrCredit { get; set; }

    [Column("patr_usac_accountNo_from")]
    [StringLength(60)]
    [Unicode(false)]
    public string? PatrUsacAccountNoFrom { get; set; }

    [Column("patr_usac_accountNo_to")]
    [StringLength(60)]
    [Unicode(false)]
    public string? PatrUsacAccountNoTo { get; set; }

    [Column("patr_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PatrType { get; set; }

    [Column("patr_invoice_no")]
    [StringLength(55)]
    [Unicode(false)]
    public string? PatrInvoiceNo { get; set; }

    [Column("patr_notes")]
    [StringLength(125)]
    [Unicode(false)]
    public string? PatrNotes { get; set; }

    [Column("patr_trxno_rev")]
    [StringLength(55)]
    [Unicode(false)]
    public string? PatrTrxnoRev { get; set; }

    [InverseProperty("BpinPatrTrxnoNavigation")]
    public virtual ICollection<BatchPartnerInvoice> BatchPartnerInvoices { get; set; } = new List<BatchPartnerInvoice>();

    [InverseProperty("PatrTrxnoRevNavigation")]
    public virtual ICollection<PaymentTransaction> InversePatrTrxnoRevNavigation { get; set; } = new List<PaymentTransaction>();

    [ForeignKey("PatrTrxnoRev")]
    [InverseProperty("InversePatrTrxnoRevNavigation")]
    public virtual PaymentTransaction? PatrTrxnoRevNavigation { get; set; }

    [InverseProperty("SecrPatrTrxnoNavigation")]
    public virtual ICollection<ServicePremiCredit> ServicePremiCredits { get; set; } = new List<ServicePremiCredit>();
}