using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("template_service_task", Schema = "mtr")]
public partial class TemplateServiceTask
{
    [Key]
    [Column("testa_id")]
    public int TestaId { get; set; }

    [Column("testa_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? TestaName { get; set; }

    [Column("testa_tety_id")]
    public int? TestaTetyId { get; set; }

    [Column("testa_group")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TestaGroup { get; set; }

    [Column("testa_callmethod")]
    [StringLength(100)]
    [Unicode(false)]
    public string? TestaCallmethod { get; set; }

    [Column("testa_seqorder")]
    public int? TestaSeqorder { get; set; }

    [Column("testa_sync_partner")]
    public char TestaSyncPartner { get; set; }

    [InverseProperty("TewoTesta")]
    public virtual ICollection<TemplateTaskWorkorder> TemplateTaskWorkorders { get; set; } = new List<TemplateTaskWorkorder>();

    [ForeignKey("TestaTetyId")]
    [InverseProperty("TemplateServiceTasks")]
    public virtual TemplateType? TestaTety { get; set; }
}