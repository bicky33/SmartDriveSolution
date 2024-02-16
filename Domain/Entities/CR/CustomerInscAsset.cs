using Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CR;

[Table("customer_insc_assets", Schema = "customer")]
[Index("CiasPoliceNumber", Name = "UQ__customer__E9035C58A4DFCBD1", IsUnique = true)]
public partial class CustomerInscAsset
{
    [Key]
    [Column("cias_creq_entityid")]
    public int CiasCreqEntityid { get; set; }

    [Column("cias_police_number")]
    [StringLength(15)]
    [Unicode(false)]
    public string CiasPoliceNumber { get; set; } = null!;

    [Column("cias_year")]
    [StringLength(4)]
    [Unicode(false)]
    public string CiasYear { get; set; } = null!;

    [Column("cias_startdate", TypeName = "datetime")]
    public DateTime? CiasStartdate { get; set; }

    [Column("cias_enddate", TypeName = "datetime")]
    public DateTime? CiasEnddate { get; set; }

    [Column("cias_current_price", TypeName = "money")]
    public decimal? CiasCurrentPrice { get; set; }

    [Column("cias_insurance_price", TypeName = "money")]
    public decimal? CiasInsurancePrice { get; set; }

    [Column("cias_total_premi", TypeName = "money")]
    public decimal? CiasTotalPremi { get; set; }

    [Column("cias_paid_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CiasPaidType { get; set; }

    [Column("cias_isNewChar")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CiasIsNewChar { get; set; }

    [Column("cias_cars_id")]
    public int? CiasCarsId { get; set; }

    [Column("cias_inty_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CiasIntyName { get; set; }

    [Column("cias_city_id")]
    public int? CiasCityId { get; set; }

    [ForeignKey("CiasCarsId")]
    [InverseProperty("CustomerInscAssets")]
    public virtual CarSeries? CiasCars { get; set; }

    [ForeignKey("CiasCityId")]
    [InverseProperty("CustomerInscAssets")]
    public virtual City? CiasCity { get; set; }

    [ForeignKey("CiasCreqEntityid")]
    [InverseProperty("CustomerInscAsset")]
    public virtual CustomerRequest CiasCreqEntity { get; set; } = null!;

    [ForeignKey("CiasIntyName")]
    [InverseProperty("CustomerInscAssets")]
    public virtual InsuranceType? CiasIntyNameNavigation { get; set; }

    [InverseProperty("CadocCreqEntity")]
    public virtual ICollection<CustomerInscDoc> CustomerInscDocs { get; set; } = new List<CustomerInscDoc>();

    [InverseProperty("CuexCreqEntity")]
    public virtual ICollection<CustomerInscExtend> CustomerInscExtends { get; set; } = new List<CustomerInscExtend>();
}