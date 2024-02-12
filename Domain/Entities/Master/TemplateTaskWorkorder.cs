using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("template_task_workorder", Schema = "mtr")]
public partial class TemplateTaskWorkorder
{
    [Key]
    [Column("tewo_id")]
    public int TewoId { get; set; }

    [Column("tewo_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? TewoName { get; set; }

    [Column("tewo_value")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TewoValue { get; set; }

    [Column("tewo_testa_id")]
    public int? TewoTestaId { get; set; }

    [ForeignKey("TewoTestaId")]
    [InverseProperty("TemplateTaskWorkorders")]
    public virtual TemplateServiceTask? TewoTesta { get; set; }
}
