using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("provinsi", Schema = "mtr")]
[Index("ProvName", Name = "UQ__provinsi__852498465E70AC22", IsUnique = true)]
public partial class Provinsi
{
    [Key]
    [Column("prov_id")]
    public int ProvId { get; set; }

    [Column("prov_name")]
    [StringLength(85)]
    [Unicode(false)]
    public string? ProvName { get; set; }

    [Column("prov_zones_id")]
    public int? ProvZonesId { get; set; }

    [InverseProperty("CityProv")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [ForeignKey("ProvZonesId")]
    [InverseProperty("Provinsis")]
    public virtual Zone? ProvZones { get; set; }

    [InverseProperty("RegpProv")]
    public virtual ICollection<RegionPlat> RegionPlats { get; set; } = new List<RegionPlat>();
}