using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.CR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("insurance_type", Schema = "mtr")]
public partial class InsuranceType
{
    [Key]
    [Column("inty_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string IntyName { get; set; } = null!;

    [Column("inty_desc")]
    [StringLength(55)]
    [Unicode(false)]
    public string? IntyDesc { get; set; }

    [InverseProperty("CiasIntyNameNavigation")]
    public virtual ICollection<CustomerInscAsset> CustomerInscAssets { get; set; } = new List<CustomerInscAsset>();

    [InverseProperty("TemiIntyNameNavigation")]
    public virtual ICollection<TemplateInsurancePremi> TemplateInsurancePremis { get; set; } = new List<TemplateInsurancePremi>();
}
