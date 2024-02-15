using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SO;

[Table("service_order_workorder", Schema = "so")]
public partial class ServiceOrderWorkorder
{
    [Key]
    [Column("sowo_id")]
    public int SowoId { get; set; }

    [Column("sowo_name")]
    [StringLength(256)]
    [Unicode(false)]
    public string? SowoName { get; set; }

    [Column("sowo_modified_date", TypeName = "datetime")]
    public DateTime? SowoModifiedDate { get; set; }

    [Column("sowo_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SowoStatus { get; set; }

    [Column("sowo_seot_id")]
    public int? SowoSeotId { get; set; }

    [ForeignKey("SowoSeotId")]
    [InverseProperty("ServiceOrderWorkorders")]
    public virtual ServiceOrderTask? SowoSeot { get; set; }
}