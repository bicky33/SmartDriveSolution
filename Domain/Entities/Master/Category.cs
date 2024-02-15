using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Master;

[Table("category", Schema = "mtr")]
public partial class Category
{


    [Key]
    [Column("cate_id")]
    public int CateId { get; set; }

    [Column("cate_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? CateName { get; set; }

    [InverseProperty("TemiCate")]
    public virtual ICollection<TemplateInsurancePremi> TemplateInsurancePremis { get; set; } = new List<TemplateInsurancePremi>();
}