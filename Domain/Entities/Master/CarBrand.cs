using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("car_brands", Schema = "mtr")]
[Index("CabrName", Name = "UQ__car_bran__750DD7D5B319F0DE", IsUnique = true)]
public partial class CarBrand
{
    [Key]
    [Column("cabr_id")]
    public int CabrId { get; set; }

    [Column("cabr_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? CabrName { get; set; }

    [InverseProperty("CarmCabr")]
    public virtual ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
}