using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("template_type", Schema = "mtr")]
[Index("TetyName", Name = "UQ__template__F5145B1292B84978", IsUnique = true)]
public partial class TemplateType
{
    [Key]
    [Column("tety_id")]
    public int TetyId { get; set; }

    [Column("tety_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TetyName { get; set; }

    [Column("tety_group")]
    [StringLength(15)]
    [Unicode(false)]
    public string? TetyGroup { get; set; }

    [InverseProperty("TestaTety")]
    public virtual ICollection<TemplateServiceTask> TemplateServiceTasks { get; set; } = new List<TemplateServiceTask>();
}
