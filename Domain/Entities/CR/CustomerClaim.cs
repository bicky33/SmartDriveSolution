using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CR;

[Table("customer_claim", Schema = "customer")]
public partial class CustomerClaim
{
    [Key]
    [Column("cucl_creq_entityid")]
    public int CuclCreqEntityid { get; set; }

    [Column("cucl_events")]
    public int? CuclEvents { get; set; }

    [Column("cucl_create_date", TypeName = "datetime")]
    public DateTime? CuclCreateDate { get; set; }

    [Column("cucl_event_price", TypeName = "money")]
    public decimal? CuclEventPrice { get; set; }

    [Column("cucl_subtotal", TypeName = "money")]
    public decimal? CuclSubtotal { get; set; }

    [Column("cucl_reason")]
    [StringLength(256)]
    [Unicode(false)]
    public string? CuclReason { get; set; }

    [ForeignKey("CuclCreqEntityid")]
    [InverseProperty("CustomerClaim")]
    public virtual CustomerRequest CuclCreqEntity { get; set; } = null!;
}