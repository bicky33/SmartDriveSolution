using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.CR;

[PrimaryKey("CuexId", "CuexCreqEntityid")]
[Table("customer_insc_extend", Schema = "customer")]
public partial class CustomerInscExtend
{
    [Key]
    [Column("cuex_id")]
    public int CuexId { get; set; }

    [Key]
    [Column("cuex_creq_entityid")]
    public int CuexCreqEntityid { get; set; }

    [Column("cuex_name")]
    [StringLength(256)]
    [Unicode(false)]
    public string? CuexName { get; set; }

    [Column("cuex_total_item")]
    public int? CuexTotalItem { get; set; }

    [Column("cuex_nominal", TypeName = "money")]
    public decimal? CuexNominal { get; set; }

    [ForeignKey("CuexCreqEntityid")]
    [InverseProperty("CustomerInscExtends")]
    public virtual CustomerInscAsset CuexCreqEntity { get; set; } = null!;
}
