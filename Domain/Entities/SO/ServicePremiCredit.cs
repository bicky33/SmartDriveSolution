using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.SO;

[PrimaryKey("SecrId", "SecrServId")]
[Table("service_premi_credit", Schema = "so")]
public partial class ServicePremiCredit
{
    [Key]
    [Column("secr_id")]
    public int SecrId { get; set; }

    [Key]
    [Column("secr_serv_id")]
    public int SecrServId { get; set; }

    [Column("secr_year")]
    [StringLength(4)]
    [Unicode(false)]
    public string? SecrYear { get; set; }

    [Column("secr_premi_debet", TypeName = "money")]
    public decimal? SecrPremiDebet { get; set; }

    [Column("secr_premi_credit", TypeName = "money")]
    public decimal? SecrPremiCredit { get; set; }

    [Column("secr_trx_date", TypeName = "datetime")]
    public DateTime? SecrTrxDate { get; set; }

    [Column("secr_duedate", TypeName = "datetime")]
    public DateTime? SecrDuedate { get; set; }

    [Column("secr_patr_trxno")]
    [StringLength(55)]
    [Unicode(false)]
    public string? SecrPatrTrxno { get; set; }

    [ForeignKey("SecrPatrTrxno")]
    [InverseProperty("ServicePremiCredits")]
    public virtual PaymentTransaction? SecrPatrTrxnoNavigation { get; set; }

    [ForeignKey("SecrServId")]
    [InverseProperty("ServicePremiCredits")]
    public virtual Servic SecrServ { get; set; } = null!;
}
