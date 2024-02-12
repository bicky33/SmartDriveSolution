using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("zones", Schema = "mtr")]
public partial class Zone
{
    [Key]
    [Column("zones_id")]
    public int ZonesId { get; set; }

    [Column("zones_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? ZonesName { get; set; }

    [InverseProperty("ProvZones")]
    public virtual ICollection<Provinsi> Provinsis { get; set; } = new List<Provinsi>();

    [InverseProperty("TemiZones")]
    public virtual ICollection<TemplateInsurancePremi> TemplateInsurancePremis { get; set; } = new List<TemplateInsurancePremi>();
}
