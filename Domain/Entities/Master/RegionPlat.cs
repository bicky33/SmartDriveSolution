using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Master;

[Table("region_plat", Schema = "mtr")]
public partial class RegionPlat
{
    [Key]
    [Column("regp_name")]
    [StringLength(3)]
    [Unicode(false)]
    public string RegpName { get; set; } = null!;

    [Column("regp_desc")]
    [StringLength(35)]
    [Unicode(false)]
    public string? RegpDesc { get; set; }

    [Column("regp_prov_id")]
    public int? RegpProvId { get; set; }

    [ForeignKey("RegpProvId")]
    [InverseProperty("RegionPlats")]
    public virtual Provinsi? RegpProv { get; set; }
}
