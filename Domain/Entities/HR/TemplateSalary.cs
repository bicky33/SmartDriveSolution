using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.HR;

[Table("template_salary", Schema = "hr")]
public partial class TemplateSalary
{
    [Key]
    [Column("tesal_id")]
    public int TesalId { get; set; }

    [Column("tesal_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? TesalName { get; set; }

    [Column("tesal_nominal", TypeName = "money")]
    public decimal? TesalNominal { get; set; }

    [Column("tesal_rate_min")]
    public double? TesalRateMin { get; set; }

    [Column("tesal_rate_max")]
    public double? TesalRateMax { get; set; }
}