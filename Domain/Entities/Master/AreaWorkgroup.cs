using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.HR;
using Domain.Entities.Partners;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("area_workgroup", Schema = "mtr")]
public partial class AreaWorkgroup
{
    [Key]
    [Column("arwg_code")]
    [StringLength(15)]
    [Unicode(false)]
    public string ArwgCode { get; set; } = null!;

    [Column("arwg_desc")]
    [StringLength(55)]
    [Unicode(false)]
    public string? ArwgDesc { get; set; }

    [Column("arwg_city_id")]
    public int? ArwgCityId { get; set; }

    [ForeignKey("ArwgCityId")]
    [InverseProperty("AreaWorkgroups")]
    public virtual City? ArwgCity { get; set; }

    [InverseProperty("EawgArwgCodeNavigation")]
    public virtual ICollection<EmployeeAreWorkgroup> EmployeeAreWorkgroups { get; set; } = new List<EmployeeAreWorkgroup>();

    [InverseProperty("PawoArwgCodeNavigation")]
    public virtual ICollection<PartnerAreaWorkgroup> PartnerAreaWorkgroups { get; set; } = new List<PartnerAreaWorkgroup>();

    [InverseProperty("SeotArwgCodeNavigation")]
    public virtual ICollection<ServiceOrderTask> ServiceOrderTasks { get; set; } = new List<ServiceOrderTask>();
}
