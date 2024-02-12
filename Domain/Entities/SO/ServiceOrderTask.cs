using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.SO;

[Table("service_order_tasks", Schema = "so")]
public partial class ServiceOrderTask
{
    [Key]
    [Column("seot_id")]
    public int SeotId { get; set; }

    [Column("seot_name")]
    [StringLength(256)]
    [Unicode(false)]
    public string? SeotName { get; set; }

    [Column("seot_startdate", TypeName = "datetime")]
    public DateTime? SeotStartdate { get; set; }

    [Column("seot_enddate", TypeName = "datetime")]
    public DateTime? SeotEnddate { get; set; }

    [Column("seot_actual_startdate", TypeName = "datetime")]
    public DateTime? SeotActualStartdate { get; set; }

    [Column("seot_actual_enddate", TypeName = "datetime")]
    public DateTime? SeotActualEnddate { get; set; }

    [Column("seot_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SeotStatus { get; set; }

    [Column("seot_arwg_code")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SeotArwgCode { get; set; }

    [Column("seot_sero_id")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SeotSeroId { get; set; }

    [ForeignKey("SeotArwgCode")]
    [InverseProperty("ServiceOrderTasks")]
    public virtual AreaWorkgroup? SeotArwgCodeNavigation { get; set; }

    [ForeignKey("SeotSeroId")]
    [InverseProperty("ServiceOrderTasks")]
    public virtual ServiceOrder? SeotSero { get; set; }

    [InverseProperty("SowoSeot")]
    public virtual ICollection<ServiceOrderWorkorder> ServiceOrderWorkorders { get; set; } = new List<ServiceOrderWorkorder>();
}
