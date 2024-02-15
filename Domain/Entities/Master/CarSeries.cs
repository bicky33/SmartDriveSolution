using Domain.Entities.CR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("car_series", Schema = "mtr")]
[Index("CarsName", Name = "UQ__car_seri__92361ED82E6545C7", IsUnique = true)]
public partial class CarSeries
{
    [Key]
    [Column("cars_id")]
    public int CarsId { get; set; }

    [Column("cars_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? CarsName { get; set; }

    [Column("cars_passenger")]
    public int? CarsPassenger { get; set; }

    [Column("cars_carm_id")]
    public int? CarsCarmId { get; set; }

    [ForeignKey("CarsCarmId")]
    [InverseProperty("CarSeries")]
    public virtual CarModel? CarsCarm { get; set; }

    [InverseProperty("CiasCars")]
    public virtual ICollection<CustomerInscAsset> CustomerInscAssets { get; set; } = new List<CustomerInscAsset>();
}