using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("template_insurance_premi", Schema = "mtr")]
public partial class TemplateInsurancePremi
{
    [Key]
    [Column("temi_id")]
    public int TemiId { get; set; }

    [Column("temi_name")]
    [StringLength(256)]
    [Unicode(false)]
    public string? TemiName { get; set; }

    [Column("temi_rate_min")]
    public double? TemiRateMin { get; set; }

    [Column("temi_rate_max")]
    public double? TemiRateMax { get; set; }

    [Column("temi_nominal")]
    public double? TemiNominal { get; set; }

    [Column("temi_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? TemiType { get; set; }

    [Column("temi_zones_id")]
    public int? TemiZonesId { get; set; }

    [Column("temi_inty_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TemiIntyName { get; set; }

    [Column("temi_cate_id")]
    public int? TemiCateId { get; set; }

    [ForeignKey("TemiCateId")]
    [InverseProperty("TemplateInsurancePremis")]
    public virtual Category? TemiCate { get; set; }

    [ForeignKey("TemiIntyName")]
    [InverseProperty("TemplateInsurancePremis")]
    public virtual InsuranceType? TemiIntyNameNavigation { get; set; }

    [ForeignKey("TemiZonesId")]
    [InverseProperty("TemplateInsurancePremis")]
    public virtual Zone? TemiZones { get; set; }
}
