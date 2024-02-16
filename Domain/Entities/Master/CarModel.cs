using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("car_models", Schema = "mtr")]
[Index("CarmName", Name = "UQ__car_mode__D13ADFA2F45F4A65", IsUnique = true)]
public partial class CarModel
{
    [Key]
    [Column("carm_id")]
    public int CarmId { get; set; }

    [Column("carm_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? CarmName { get; set; }

    [Column("carm_cabr_id")]
    public int? CarmCabrId { get; set; }

    [InverseProperty("CarsCarm")]
    public virtual ICollection<CarSeries> CarSeries { get; set; } = new List<CarSeries>();

    [ForeignKey("CarmCabrId")]
    [InverseProperty("CarModels")]
    public virtual CarBrand? CarmCabr { get; set; }
}