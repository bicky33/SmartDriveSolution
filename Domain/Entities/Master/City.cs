using Domain.Entities.CR;
using Domain.Entities.Partners;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("cities", Schema = "mtr")]
[Index("CityName", Name = "UQ__cities__1AA4F7B507540D76", IsUnique = true)]
public partial class City
{
    [Key]
    [Column("city_id")]
    public int CityId { get; set; }

    [Column("city_name")]
    [StringLength(85)]
    [Unicode(false)]
    public string? CityName { get; set; }

    [Column("city_prov_id")]
    public int? CityProvId { get; set; }

    [InverseProperty("ArwgCity")]
    public virtual ICollection<AreaWorkgroup> AreaWorkgroups { get; set; } = new List<AreaWorkgroup>();

    [ForeignKey("CityProvId")]
    [InverseProperty("Cities")]
    public virtual Provinsi? CityProv { get; set; }

    [InverseProperty("CiasCity")]
    public virtual ICollection<CustomerInscAsset> CustomerInscAssets { get; set; } = new List<CustomerInscAsset>();

    [InverseProperty("PartCity")]
    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();

    [InverseProperty("UsdrCity")]
    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}