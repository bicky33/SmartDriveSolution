using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SO;

[Table("service_premi", Schema = "so")]
public partial class ServicePremi
{
    [Key]
    [Column("semi_serv_id")]
    public int SemiServId { get; set; }

    [Column("semi_premi_debet", TypeName = "money")]
    public decimal? SemiPremiDebet { get; set; }

    [Column("semi_premi_credit", TypeName = "money")]
    public decimal? SemiPremiCredit { get; set; }

    [Column("semi_paid_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SemiPaidType { get; set; }

    [Column("semi_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SemiStatus { get; set; }

    [Column("semi_modified_date", TypeName = "datetime")]
    public DateTime? SemiModifiedDate { get; set; }

    [ForeignKey("SemiServId")]
    [InverseProperty("ServicePremi")]
    public virtual Service SemiServ { get; set; } = null!;
}