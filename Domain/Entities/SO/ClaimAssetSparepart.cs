using Domain.Entities.Partners;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SO;

[Table("claim_asset_sparepart", Schema = "so")]
public partial class ClaimAssetSparepart
{
    [Key]
    [Column("casp_id")]
    public int CaspId { get; set; }

    [Column("casp_item_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? CaspItemName { get; set; }

    [Column("casp_quantity")]
    public int? CaspQuantity { get; set; }

    [Column("casp_item_price", TypeName = "money")]
    public decimal? CaspItemPrice { get; set; }

    [Column("casp_subtotal", TypeName = "money")]
    public decimal? CaspSubtotal { get; set; }

    [Column("casp_part_entityid")]
    public int? CaspPartEntityid { get; set; }

    [Column("casp_sero_id")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CaspSeroId { get; set; }

    [Column("casp_created_date", TypeName = "datetime")]
    public DateTime? CaspCreatedDate { get; set; }

    [ForeignKey("CaspPartEntityid")]
    [InverseProperty("ClaimAssetSpareparts")]
    public virtual Partner? CaspPartEntity { get; set; }

    [ForeignKey("CaspSeroId")]
    [InverseProperty("ClaimAssetSpareparts")]
    public virtual ServiceOrder? CaspSero { get; set; }
}